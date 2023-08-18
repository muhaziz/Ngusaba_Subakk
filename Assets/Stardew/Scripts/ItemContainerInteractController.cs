using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerInteractController : MonoBehaviour
{
    ItemContainer targetItemContainer;
    InventoryController inventoryController;
    [SerializeField] ItemContainerPanel itemContainerPanel;
    Transform openedChest;
    [SerializeField] float maxDistance = 0.8f;

    private void Awake()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    private void Update()
    {
        if (openedChest != null)
        {
            float distance = Vector2.Distance(openedChest.position, transform.position);
            Debug.Log("Distance to chest: " + distance); // Ini akan menunjukkan jarak ke chest di Console

            if (distance > maxDistance)
            {
                Debug.Log("Closing Chest UI due to distance."); // Ini akan muncul di Console saat kondisi terpenuhi
                GetComponent<InventoryController>().CloseChestUI();
            }
        }
    }

    public void Open(ItemContainer itemContainer, Transform _openedChest)
    {
        targetItemContainer = itemContainer;
        itemContainerPanel.inventory = targetItemContainer;
        inventoryController.Open();
        itemContainerPanel.gameObject.SetActive(true);
        openedChest = _openedChest;
    }

    public void Close()
    {
        inventoryController.Close();
        itemContainerPanel.gameObject.SetActive(false);
        openedChest = null;
    }
}
