using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{

    // public GameObject notePrefab;
    // public float minSpawnRate = 1f;
    // public float maxSpawnRate = 3f;

    // private float nextSpawnInterval;
    // private float timeSinceLastSpawn;

    // private void Start()
    // {
    //     SetRandomSpawnInterval();
    // }

    // private void Update()
    // {
    //     timeSinceLastSpawn += Time.deltaTime;

    //     if (timeSinceLastSpawn >= nextSpawnInterval)
    //     {
    //         SpawnNote();
    //         timeSinceLastSpawn = 0f;
    //         SetRandomSpawnInterval();
    //     }
    // }

    // void SetRandomSpawnInterval()
    // {
    //     nextSpawnInterval = Random.Range(minSpawnRate, maxSpawnRate);
    // }

    // void SpawnNoteFromRandomSpawner()
    // {
    //     if (notePrefab != null) // Tambahkan pemeriksaan ini
    //     {
    //         var note = Instantiate(notePrefab, randomPosition, Quaternion.identity);
    //         note.GetComponent<NoteBehavior>().ShowNote();
    //     }
    //     else
    //     {
    //         Debug.LogError("notePrefab is null!");
    //     }
    // }
}
