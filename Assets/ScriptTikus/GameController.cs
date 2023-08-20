using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum GameState
    {
        Tutorial,
        Playing,
        Paused,
        Ended
    }
    public GameState currentState = GameState.Tutorial;

    [Header("Mole Settings")]
    public MoleBehaviour[] moles;
    public float minTimeBetweenSpawns = 1.0f;
    public float maxTimeBetweenSpawns = 3.0f;

    [Header("Game Duration & Score")]
    public float gameTime = 60.0f;
    private float currentTime;
    public int initialScore = 5;
    public int maxScore = 10;
    private int score = 0;
    private bool gameEnded = false;

    [Header("UI Components")]
    public Image resultImage;
    public TextMeshProUGUI timerText;
    public Slider scoreSlider;
    public GameObject resultMenu;
    public TextMeshProUGUI scoreText, highscoreText, ratingText, statisticsText;
    public string essentialSceneToLoad;
    public string mainSceneToLoad;
    public Vector3 playerStartPosition;
    public GameObject player;


    [Header("Score & Feedback")]
    public float sliderDecreaseRate = 0.05f;
    public float sliderIncreaseRate = 0.05f;
    public float sliderLerpSpeed = 5.0f;
    private float intendedSliderValue;
    private Coroutine hideFeedbackCoroutine;

    [Header("Game Statistics")]
    public int moleHit;
    public int moleMissed;
    public int redMoleHit;

    [Header("Pause Menu")]
    public GameObject pauseMenu; // Drag and drop your pause menu panel here via the inspector

    private bool isPaused = false;
    private bool gameStarted = false; // Untuk mengetahui apakah game sudah dimulai atau belum

    private void Start()
    {
        if (currentState == GameState.Tutorial)
        {
            // Jika dalam mode tutorial, jangan mulai game
            gameStarted = false;
            return;
        }

        BeginGame();
    }


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    public void BeginGame()
    {
        gameStarted = true; // Mulai game

        // ... (sisa dari kode BeginGame Anda)
        score = initialScore;
        intendedSliderValue = score;
        scoreSlider.maxValue = maxScore;
        scoreSlider.value = score;
        currentTime = gameTime;
        UpdateTimerDisplay();

        InvokeRepeating("SpawnRandomMole", 0, Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));
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

    // Fungsi untuk spawn mole secara acak
    void SpawnRandomMole()
    {
        if (currentState != GameState.Playing) return;

        int randomIndex = Random.Range(0, moles.Length);
        moles[randomIndex].ShowMole();
    }

    // Update skor saat pemain melakukan interaksi dengan mole
    public void UpdateScore(int value)
    {
        //Debug.Log("Menambah skor sebanyak: " + value);
        score += value;
        score = Mathf.Clamp(score, 0, maxScore);
        AdjustSliderValue(value * sliderIncreaseRate);
        scoreText.text = "Score: " + score;
    }

    // Fungsi untuk menyesuaikan skor slider
    void AdjustSliderValue(float amount)
    {
        intendedSliderValue += amount;
        //Debug.Log("intendedSliderValue set to: " + intendedSliderValue);
        intendedSliderValue = Mathf.Clamp(intendedSliderValue, 0, maxScore);
    }

    // Coroutine untuk penurunan skor konstan
    IEnumerator ConstantDecrease()
    {
        while (true)
        {
            AdjustSliderValue(-sliderDecreaseRate);
            yield return new WaitForSeconds(1.0f);
        }
    }

    // Coroutine untuk menghitung durasi game
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

    // Update tampilan waktu
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

    // Fungsi yang dipanggil ketika game berakhir
    private void EndGame()
    {

        if (gameEnded) return; // Jika game sudah berakhir, hentikan 
        gameEnded = true;
        CancelInvoke("SpawnRandomMole");
        StopCoroutine("ConstantDecrease");
        StopAllCoroutines();  // Hentikan semua coroutines yang sedang berjalan
        foreach (var mole in moles) mole.HideMole();
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

        // Perbarui teks statistik dengan informasi redMoleHit:
        statisticsText.text = "Mole Dipukul: " + moleHit + "\nMole Merah Dipukul: " + redMoleHit + "\nMole Terlewat: " + moleMissed;
    }

    public void MoleWasHit()
    {
        moleHit++;
    }

    public void MoleWasMissed()
    {
        moleMissed++;
    }
    public void RedMoleWasHit()
    {
        redMoleHit++;
    }

    public void ChangeScene()
    {

        SceneManager.LoadScene(essentialSceneToLoad);
        SceneManager.LoadScene(mainSceneToLoad, LoadSceneMode.Additive);

        // Pindahkan player ke posisi awal setelah scene di-load
        player.transform.position = playerStartPosition;
    }
    public void RestartScene()
    {

        SceneManager.LoadScene("Challange1");

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
