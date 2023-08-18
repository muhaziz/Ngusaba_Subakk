using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{

    [SerializeField] GameObject storePanel;
    //[SerializeField] GameObject inventoryPanel;

    public void BeginTrading()
    {
        storePanel.SetActive(true);
        //inventoryPanel.SetActive(true);
    }

    public void StopTrading()
    {
        storePanel.SetActive(false);
        //inventoryPanel.SetActive(false);
    }
}
