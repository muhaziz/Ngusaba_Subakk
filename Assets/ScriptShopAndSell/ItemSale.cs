using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSale", menuName = "NgusabaSubak/ItemSale", order = 0)]
public class ItemSale : ScriptableObject
{
    public string itemName;
    public int price;
    public Sprite icon;
    public Item actualItem;
}
