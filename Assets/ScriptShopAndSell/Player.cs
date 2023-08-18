using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int Money;
    public ItemContainer Inventory;
    public TextMeshProUGUI playerMoneyText;

    private void Start()
    {
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {

        if (playerMoneyText)
        {
            playerMoneyText.text = string.Format("Rp. {0:N0}", Money);
        }


    }

    private void Update()
    {
        // Ini opsional, jika Anda ingin secara terus menerus memperbarui UI setiap frame.
        // Jika Anda memanggil UpdateMoneyUI() di tempat lain setiap kali uang berubah, Anda tidak perlu memanggilnya di Update().
        UpdateMoneyUI();
    }
}
