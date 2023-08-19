using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class cutsceneManager : MonoBehaviour
{

    [SerializeField] private VideoPlayer cutsceneVideo;
    [SerializeField] private Button skipButton;
    public Canvas mainUICanvas; // Drag and drop MainUICanvas dari Inspector
    public Canvas cutsceneCanvas; // Drag and drop CutsceneCanvas dari Inspector
    private bool hasPlayedCutscene = false;

    // Dijalankan oleh CutsceneTrigger
    public void PlayCutscene()
    {
        if (!hasPlayedCutscene)
        {
            // Nonaktifkan Main UI Canvas
            mainUICanvas.gameObject.SetActive(false);

            // Aktifkan Cutscene UI Canvas (jika Anda memerlukannya)
            cutsceneCanvas.gameObject.SetActive(true);

            cutsceneVideo.Play();
            skipButton.gameObject.SetActive(true);
            skipButton.onClick.AddListener(SkipCutscene);
            cutsceneVideo.loopPointReached += EndCutscene;
            hasPlayedCutscene = true;
        }
    }

    public void SkipCutscene()
    {
        cutsceneVideo.Stop();
        EndCutscene(cutsceneVideo);
    }

    private void EndCutscene(VideoPlayer vp)
    {
        skipButton.gameObject.SetActive(false);
        skipButton.onClick.RemoveListener(SkipCutscene);

        // Aktifkan kembali Main UI Canvas
        mainUICanvas.gameObject.SetActive(true);

        // Nonaktifkan Cutscene UI Canvas
        cutsceneCanvas.gameObject.SetActive(false);

        // Anda mungkin ingin menambahkan logika lain di sini, misalnya menonaktifkan trigger.
    }
}
