using UnityEngine;

public class NoteSpawnerControl : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject[] spawners;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 3f;

    private float nextSpawnInterval;
    private float timeSinceLastSpawn;

    private void Start()
    {
        SetRandomSpawnInterval();
    }

    private void Update()
    {
        if (GameRythemControl.instance.currentState != GameRythemControl.GameState.Playing ||
      GameRythemControl.instance.currentState == GameRythemControl.GameState.Ended)
            return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= nextSpawnInterval)
        {
            SpawnNoteFromRandomSpawner();
            timeSinceLastSpawn = 0f;
            SetRandomSpawnInterval();
        }
    }

    void SetRandomSpawnInterval()
    {
        nextSpawnInterval = Random.Range(minSpawnRate, maxSpawnRate);
    }

    void SpawnNoteFromRandomSpawner()
    {
        int randomIndex = Random.Range(0, spawners.Length);
        Instantiate(notePrefab, spawners[randomIndex].transform.position, Quaternion.identity);
    }
}
