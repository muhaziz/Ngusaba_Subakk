using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI quantityText;
    public Button increaseButton;
    public Button decreaseButton;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    private ItemSale item;
    private Player player;
    private int selectedQuantity = 1;

    private void Awake()
    {
        increaseButton.onClick.AddListener(IncreaseQuantity);
        decreaseButton.onClick.AddListener(DecreaseQuantity);
        buyButton.onClick.AddListener(BuyItem);
    }
    public void Setup(ItemSale newItem, Player playerReference)
    {
        Debug.Log($"Item: {newItem}");
        Debug.Log($"Player: {playerReference}");
        Debug.Log($"Item Icon: {itemIcon}");
        item = newItem;
        player = playerReference;

        itemIcon.sprite = item.icon;
        itemNameText.text = item.itemName;
        UpdateUI();
    }

    void IncreaseQuantity()
    {
        selectedQuantity++;
        UpdateUI();
    }

    void DecreaseQuantity()
    {
        selectedQuantity = Mathf.Max(1, selectedQuantity - 1); // Pastikan kuantitas minimal adalah 1
        UpdateUI();
    }

    void UpdateUI()
    {
        quantityText.text = selectedQuantity.ToString();
        priceText.text = string.Format("Rp. {0:N0}", item.price * selectedQuantity);
    }

    void BuyItem()
    {
        if (player.Money >= item.price * selectedQuantity) // Pemain memiliki cukup uang
        {
            player.Money -= item.price * selectedQuantity;
            player.Inventory.Add(item.actualItem, selectedQuantity);
            selectedQuantity = 1; // Reset kuantitas
            UpdateUI();
        }
        else
        {
            // Tampilkan pesan bahwa pemain tidak memiliki cukup uang
        }
    }
}
