using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject ChestPanel;
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject SellPanel;
    [SerializeField] private GameObject chestUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (panel.activeInHierarchy == false)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        statusPanel.SetActive(true);
        toolbarPanel.SetActive(false);
        storePanel.SetActive(false);
        SellPanel.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
        statusPanel.SetActive(false);
        toolbarPanel.SetActive(true);
        ChestPanel.SetActive(false);
        storePanel.SetActive(false);
    }

    public void OpenChestUI()
    {
        chestUI.SetActive(true);
    }

    public void CloseChestUI()
    {
        chestUI.SetActive(false);
    }

}
