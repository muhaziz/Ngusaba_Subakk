using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueQuest2 : MonoBehaviour
{
    [SerializeField] GameObject Dialoguepanel;
    //[SerializeField] GameObject inventoryPanel;
    public TextMeshProUGUI dialogueText;
    public DialogueContainer dialogueContainer;

    public void startQuest()
    {
        Dialoguepanel.SetActive(true);
        if (dialogueContainer.line.Count > 0) // cek apakah ada teks di dalam list
        {
            dialogueText.text = dialogueContainer.line[0];
        }


    }

    public void stopQuest()
    {
        Dialoguepanel.SetActive(false);
        //inventoryPanel.SetActive(false);
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
