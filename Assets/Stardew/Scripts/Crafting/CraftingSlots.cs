using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{

    public Image itemImage;


    public void SetItem(Item item)
    {
        Sprite itemSprite = item.icon;
    }
}
