using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftResultSlot : MonoBehaviour
{

    public Image itemImage;

    private Item item;  // Item yang akan dibuat

    public void SetResult(Item result)
    {
        item = result;
        if (item != null)
        {
            itemImage.sprite = item.icon;
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.gameObject.SetActive(false);
        }
    }

    public void RetrieveResult()
    {
        if (item != null)
        {
            // Tambahkan item ke inventory pemain
            GameManager.instance.inventoryContainer.Add(item);
            // Kosongkan slot hasil
            SetResult(null);
        }
    }
}


