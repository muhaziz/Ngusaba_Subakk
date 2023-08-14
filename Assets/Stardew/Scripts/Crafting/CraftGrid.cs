using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftGrid : MonoBehaviour
{
    public CraftingSlot[] slots = new CraftingSlot[9];

    void Start()
    {
        // Assuming that the crafting slots are children of this grid
        for (int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).GetComponent<CraftingSlot>();
        }
    }

    public void UpdateGrid(Item[] items)
    {
        // Update the grid with the given items
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetItem(items[i]);
        }
    }
}


