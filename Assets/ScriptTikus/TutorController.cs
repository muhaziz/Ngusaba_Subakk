using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorController : MonoBehaviour
{
    public GameObject[] tutorialSteps; // Drag semua panel tutorial Anda ke dalam array ini melalui inspector.
    private int currentStep = 0;
    public GameObject gameUI; // UI utama untuk game Anda.
    public GameObject tutorialUI; // UI utama untuk tutorial.

    private void Start()
    {
        // Asumsikan Anda ingin mulai dengan tutorial
        for (int i = 0; i < tutorialSteps.Length; i++)
        {
            tutorialSteps[i].SetActive(false);
        }
        tutorialSteps[0].SetActive(true); // Mulai dengan langkah pertama.
        gameUI.SetActive(false); // Pastikan UI game tidak aktif.
    }

    public void NextStep()
    {
        tutorialSteps[currentStep].SetActive(false);
        currentStep++;
        if (currentStep < tutorialSteps.Length)
        {
            tutorialSteps[currentStep].SetActive(true);
        }
        else
        {
            // Selesai dengan tutorial, mulai game.
            StartGame();
        }
    }

    void StartGame()
    {
        tutorialUI.SetActive(false);
        gameUI.SetActive(true);
        // Logika lain untuk memulai permainan Anda.
    }

}
