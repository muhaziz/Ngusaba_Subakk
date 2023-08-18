using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    public Transform sellItemContainer; // parent transform untuk semua item yang akan dijual
    public GameObject sellItemPrefab; // prefab untuk UI item yang akan dijual
    private Dictionary<Item, GameObject> instantiatedItemObjects = new Dictionary<Item, GameObject>();

    private Player player;

    private void Start()
    {
        player = GameManager.instance.player1;
        //  Debug.Log($"Referensi Pemain: {player}");
        PopulateSellItems();
    }

    public void PopulateSellItems()
    {

        // Tampilkan atau perbarui semua item dari Inventory pemain
        foreach (ItemSlot slot in player.Inventory.slots)
        {
            if (slot.item == null) continue;

            GameObject itemObj;

            // Jika item ini sudah ada di UI
            if (instantiatedItemObjects.ContainsKey(slot.item))
            {
                itemObj = instantiatedItemObjects[slot.item];

                // Jika jumlahnya adalah 0, sembunyikan item dari UI
                if (slot.count <= 0)
                {
                    itemObj.SetActive(false);
                    continue;
                }
                else
                {
                    itemObj.SetActive(true); // Pastikan item ditampilkan
                }
            }
            else // Jika item ini belum ada di UI, instantiate dan tambahkan ke dictionary
            {
                itemObj = Instantiate(sellItemPrefab, sellItemContainer);
                instantiatedItemObjects[slot.item] = itemObj;
            }

            ItemSell itemSell = itemObj.GetComponent<ItemSell>();
            itemSell.Setup(slot.item, player); // Gunakan setup yang ada untuk menginisialisasi UI
        }

        // Membersihkan item yang sudah tidak ada di inventory lagi
        List<Item> itemsToRemove = new List<Item>();

        foreach (var entry in instantiatedItemObjects)
        {
            if (!player.Inventory.slots.Exists(s => s.item == entry.Key && s.count > 0))
            {
                itemsToRemove.Add(entry.Key);
                Destroy(entry.Value);
            }
        }

        foreach (var item in itemsToRemove)
        {
            instantiatedItemObjects.Remove(item);
        }
    }
}