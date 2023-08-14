using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeCraft", menuName = "ngusaba_Subak/RecipeCraft", order = 0)]
public class RecipeCraft : ScriptableObject
{
    public const int GRID_SIZE = 3;
    public Item[,] recipe = new Item[GRID_SIZE, GRID_SIZE];
    public List<Item> ingredients; // Daftar item yang diperlukan untuk membuat item hasil
    public Item result;

    public bool MatchesGrid(Item[,] craftingGrid)
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                if (craftingGrid[i, j] != recipe[i, j])
                {
                    return false; // Jika ada item yang tidak cocok, resep ini tidak cocok
                }
            }
        }

        return true; // Jika semua item cocok, resep ini cocok
    }

}

