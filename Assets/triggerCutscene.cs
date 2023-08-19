using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class triggerCutscene : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cutsceneManager manager = FindObjectOfType<cutsceneManager>();
            if (manager)
            {
                manager.PlayCutscene();
                gameObject.SetActive(false);  // Menonaktifkan trigger setelah cutscene dipicu
            }
        }
    }
}
