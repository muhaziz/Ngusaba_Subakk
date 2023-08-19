using TMPro;
using UnityEngine;
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
    public int initialScore = 5;
    public int maxScore = 10;
    public GameObject resultImage;

    private bool gameEnded = false;

    [Header("UI Components")]
    public Image resultImageUI;  // Kita tambahkan ini dari GameRythemControl
    public TextMeshProUGUI timerText;
    public Slider scoreSlider;
    public GameObject resultMenu;
    public TextMeshProUGUI scoreText, highscoreText, ratingText;

    [Header("Score & Feedback")]
    public float sliderDecreaseRate = 0.05f;
    public float sliderIncreaseRate = 0.05f;
    public float sliderLerpSpeed = 5.0f;
    private float intendedSliderValue;

    private bool gameStarted = false;

    private void Start()
    {
        if (currentState == GameState.Tutorial)
        {
            gameStarted = false;
            return;
        }

        BeginGame();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Kita tambahkan ini dari GameRythemControl
        }
        else
        {
            Destroy(gameObject);
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

    private void Update()
    {
        if (!gameStarted)
            return;

        intendedSliderValue -= sliderDecreaseRate * Time.deltaTime;
        scoreSlider.value = Mathf.MoveTowards(scoreSlider.value, intendedSliderValue, sliderLerpSpeed * Time.deltaTime);

        if (scoreSlider.value <= 0 && currentState == GameState.Playing && !gameEnded)
        {
            EndGame();
        }

        // Kode dari GameRythemControl
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

    public void UpdateScore(int value)
    {
        score += value;
        score = Mathf.Clamp(score, 0, maxScore);
        AdjustSliderValue(value * sliderIncreaseRate);
        scoreText.text = "Score: " + score;

        // Kita tambahkan ini dari GameRythemControl
        Debug.Log("Current Score: " + score);
    }

    void AdjustSliderValue(float amount)
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

        if (score >= 10) ratingText.text = "Rating: A";
        else if (score >= 7) ratingText.text = "Rating: B";

        // Kita tambahkan ini dari GameRythemControl
        ShowResult();
    }

    void ShowResult()
    {
        resultImageUI.SetActive(true);
        // Anda bisa menambahkan lebih banyak detail seperti skor, dsb. di sini
    }
}
