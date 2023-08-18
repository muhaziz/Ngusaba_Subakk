using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStoreUI : Interactable
{
    public GameObject shopUI;
    public override void Interact(Character character)
    {
        GameObject shopUI = GameObject.Find("ShopUI");
        shopUI.SetActive(true);
    }
}
