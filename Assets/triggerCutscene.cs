using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class triggerCutscene : MonoBehaviour
{

    [SerializeField] private VideoPlayer cutsceneVideo;
    [SerializeField] private Button skipButton;

    private bool hasPlayedCutscene = false;

    // Dijalankan oleh CutsceneTrigger
    public void PlayCutscene()
    {
        if (!hasPlayedCutscene)
        {
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
        // Anda mungkin ingin menambahkan logika lain di sini, misalnya menonaktifkan trigger.
    }
}
