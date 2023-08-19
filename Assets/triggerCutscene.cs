using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerCutscene : MonoBehaviour
{
  
    [SerializeField] private VideoPlayer cutsceneVideo;
    [SerializeField] private Button skipButton; // Referensi ke tombol skip cutscene

    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayed)
        {
            PlayCutscene();
        }
    }

    private void PlayCutscene()
    {
        cutsceneVideo.Play();
        hasPlayed = true;
        skipButton.gameObject.SetActive(true); // Aktifkan tombol skip saat cutscene dimulai
        skipButton.onClick.AddListener(SkipCutscene); // Tambahkan listener ke tombol skip

        cutsceneVideo.loopPointReached += DestroyTrigger;
    }

    private void SkipCutscene()
    {
        cutsceneVideo.Stop(); // Hentikan video
        DestroyTrigger(cutsceneVideo); // Panggil fungsi yang akan menghancurkan trigger
    }

    private void DestroyTrigger(VideoPlayer vp)
    {
        skipButton.gameObject.SetActive(false); // Nonaktifkan tombol skip setelah video selesai
        skipButton.onClick.RemoveListener(SkipCutscene); // Lepaskan listener dari tombol skip
        Destroy(this.gameObject);
    }
}
