using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSell : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI quantityText;
    public Button increaseButton;
    public Button decreaseButton;
    public TextMeshProUGUI priceText;
    public Button sellButton;

    private Item item;
    private Player player;
    private int selectedQuantity = 1;

    private void Awake()
    {
        increaseButton.onClick.AddListener(IncreaseQuantity);
        decreaseButton.onClick.AddListener(DecreaseQuantity);
        sellButton.onClick.AddListener(SellItem);
    }

    public void Setup(Item itemToSell, Player playerReference)
    {
        item = itemToSell;
        player = playerReference;

        itemIcon.sprite = item.icon;
        itemNameText.text = item.Name;
        UpdateUI();
    }

    void IncreaseQuantity()
    {
        selectedQuantity++;
        UpdateUI();
    }

    void DecreaseQuantity()
    {
        selectedQuantity = Mathf.Max(1, selectedQuantity - 1);
        UpdateUI();
    }

    void UpdateUI()
    {
        quantityText.text = selectedQuantity.ToString();
        priceText.text = string.Format("Rp. {0:N0}", item.price * selectedQuantity);
    }

    void SellItem()
    {
        // Cek apakah item yang ingin dijual ada di inventory
        if (player.Inventory.CheckItem(item, selectedQuantity))
        {
            // Tambah uang ke player sesuai harga jual item
            player.Money += item.price * selectedQuantity;  // Menggunakan price sebagai contoh, Anda mungkin memiliki harga jual terpisah.

            // Hapus item dari inventory
            player.Inventory.Remove(item, selectedQuantity);

            // Update UI jika perlu
            UpdateUI();
            player.UpdateMoneyUI();
        }
        else
        {
            // Anda bisa menambahkan feedback ke pemain di sini, misalnya dengan menampilkan pesan bahwa ia tidak memiliki cukup item untuk dijual.
        }
    }
}

