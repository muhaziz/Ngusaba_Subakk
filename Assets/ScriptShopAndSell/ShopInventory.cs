using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopInventory", menuName = "NgusabaSubak/ShopInventory", order = 0)]
public class ShopInventory : ScriptableObject
{

    public List<ItemSale> itemsForSale;
}
