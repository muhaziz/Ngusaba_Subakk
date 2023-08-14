using UnityEngine;

public class GameRythemControl : MonoBehaviour
{
    public static GameRythemControl instance;
    public int score;
    public GameObject resultImage; // Drag and drop ResultImage dari hierarchy ke sini melalui inspector
    public float gameTime = 60f; // contoh durasi game 60 detik
    private bool gameEnded = false;

    private void Update()
    {
        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0f)
            {
                gameTime = 0f;
                EndGame();
            }
        }
    }

    void EndGame()
    {
        gameEnded = true;
        ShowResult();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        Debug.Log("Current Score: " + score);
    }
    void ShowResult()
    {
        resultImage.SetActive(true);
        // Anda bisa menambahkan lebih banyak detail seperti skor, dsb. di sini
    }
}
