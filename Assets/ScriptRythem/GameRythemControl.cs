using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRythemControl : MonoBehaviour
{
    public static GameRythemControl instance;

    public enum GameState
    {
        Tutorial,
        Playing,
        Paused,
        Ended
    }
    public GameState currentState = GameState.Tutorial;

    [Header("Game Duration & Score")]
    public float gameTime = 60.0f;
    private float currentTime;
    private int score;
    public int initialScore = 5;
    public int maxScore = 10;
    public GameObject resultImage;

    private bool gameEnded = false;

    [Header("UI Components")]
    public Image resultImageUI;
    public TextMeshProUGUI timerText;
    public Slider scoreSlider;
    public GameObject resultMenu;
    public TextMeshProUGUI scoreText, highscoreText, ratingText;

    [Header("Score & Feedback")]
    public float sliderDecreaseRate = 0.05f;
    public float sliderIncreaseRate = 0.05f;
    public float sliderLerpSpeed = 5.0f;
    private float intendedSliderValue;

    [Header("Pause Menu")]
    public GameObject pauseMenu; // Drag and drop your pause menu panel here via the inspector

    private bool isPaused = false;

    private bool gameStarted = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // Jika instance lama ada, hancurkan instance yang baru ini.
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        if (currentState == GameState.Tutorial)
        {
            gameStarted = false;
            return;
        }
        BeginGame();
    }

    private void Update()
    {
        if (currentState != GameState.Playing) return;  // Tambahkan baris ini

        //  if (!gameStarted) return;

        intendedSliderValue -= sliderDecreaseRate * Time.deltaTime;
        scoreSlider.value = Mathf.MoveTowards(scoreSlider.value, intendedSliderValue, sliderLerpSpeed * Time.deltaTime);

        if (scoreSlider.value <= 0 && currentState == GameState.Playing && !gameEnded)
        {
            EndGame();
        }

        if (!gameEnded)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0f)
            {
                gameTime = 0f;
                EndGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void BeginGame()
    {
        gameStarted = true;

        score = initialScore;
        intendedSliderValue = score;
        scoreSlider.maxValue = maxScore;
        scoreSlider.value = score;
        currentTime = gameTime;
        UpdateTimerDisplay();

        StartCoroutine(ConstantDecrease());
        StartCoroutine(GameTimer());
    }

    public void UpdateScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, maxScore);
        AdjustSliderValue(value * sliderIncreaseRate);
        scoreText.text = "Score: " + score;
        Debug.Log("Current Score: " + score);
    }

    private void AdjustSliderValue(float amount)
    {
        intendedSliderValue += amount;
        intendedSliderValue = Mathf.Clamp(intendedSliderValue, 0, maxScore);
    }

    IEnumerator ConstantDecrease()
    {
        while (true)
        {
            AdjustSliderValue(-sliderDecreaseRate);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator GameTimer()
    {
        while (currentTime > 0 && currentState == GameState.Playing)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
            UpdateTimerDisplay();
        }
        EndGame();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    public void StartGameAfterTutorial()
    {
        currentState = GameState.Playing;
        BeginGame();
    }

    private void EndGame()
    {
        if (gameEnded) return;
        currentState = GameState.Ended;
        gameEnded = true;
        StopCoroutine("ConstantDecrease");
        StopAllCoroutines();
        resultMenu.SetActive(true);

        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        highscoreText.text = "Rekor Terbaik: " + highscore;

        if (score >= 500) ratingText.text = "Rating: A";
        else if (score >= 300) ratingText.text = "Rating: B";

        ShowResult();
    }

    private void ShowResult()
    {
        resultImageUI.gameObject.SetActive(true);

    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Just in case the game is paused, ensure the time is reset to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // This reloads the current scene

        currentState = GameState.Playing;
        pauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // This pauses the game
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // This resumes the game
        pauseMenu.SetActive(false);
    }
}