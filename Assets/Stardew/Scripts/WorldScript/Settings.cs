using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public string sceneName; // The name of the scene you want to load

    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_Dropdown graphicsQualityDropdown;
    public Slider audioVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;
    public AudioMixer audioMixer;

    private Resolution[] resolutions;
    private string[] qualityLevels;

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public GameObject pauseMenuUI; // Objek yang mewakili UI menu jeda
    private bool isPaused = false; // Status jeda

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Tombol Escape sebagai tombol jeda
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

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Mengaktifkan kembali waktu permainan normal
        isPaused = false;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu permainan
        isPaused = true;
    }

    public void QuitGame()
    {

        Application.Quit();
    }

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progres;
    public void loadLevel(string levelName)
    {
        StartCoroutine(LoadAsynchronisly(levelName));
    }
    IEnumerator LoadAsynchronisly(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progres.text = progress * 100f + "%";
            yield return null;
        }
    }

    private void Start()
    {
        LoadResolutions();
        LoadQualityLevels();
        LoadAudioVolume();
        LoadSFXVolume();
        LoadBGMVolume();


    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //Debug.Log("Resolution set to " + resolution.width + "x" + resolution.height);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        //Debug.Log("Fullscreen set to " + isFullscreen);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        //Debug.Log("Graphics quality set to " + qualityIndex);
    }

    public void SetAudioVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        //Debug.Log("Audio volume set to " + volume);
    }

    private void LoadResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> resolutionOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void LoadQualityLevels()
    {
        qualityLevels = QualitySettings.names;
        graphicsQualityDropdown.ClearOptions();
        graphicsQualityDropdown.AddOptions(qualityLevels.ToList());
        graphicsQualityDropdown.value = QualitySettings.GetQualityLevel();
        graphicsQualityDropdown.RefreshShownValue();
    }

    private void LoadAudioVolume()
    {
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        audioVolumeSlider.value = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        //Debug.Log("SFX volume set to " + volume);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
        //Debug.Log("BGM volume set to " + volume);
    }

    private void LoadSFXVolume()
    {
        float volume;
        audioMixer.GetFloat("SFXVolume", out volume);
        sfxVolumeSlider.value = volume;
    }

    private void LoadBGMVolume()
    {
        float volume;
        audioMixer.GetFloat("BGMVolume", out volume);
        bgmVolumeSlider.value = volume;
    }

}



