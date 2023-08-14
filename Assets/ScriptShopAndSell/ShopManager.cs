using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public ShopInventory shopInventory; // Daftar barang yang dijual
    public GameObject itemEntryPrefab;
    public Transform contentPanel;
    public Player player;

    void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (ItemSale item in shopInventory.itemsForSale)
        {
            GameObject newEntry = Instantiate(itemEntryPrefab, contentPanel);
            newEntry.GetComponent<ShopUI>().Setup(item, player);
        }
    }
}
