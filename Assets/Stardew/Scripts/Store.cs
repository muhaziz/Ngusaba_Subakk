using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{
    public GameObject shopUI;

    public override void Interact(Character character)
    {
        shopUI.SetActive(true);
    }
}
