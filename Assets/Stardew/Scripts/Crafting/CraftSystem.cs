using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public ItemContainer playerInventory;  // Inventaris pemain
    public List<RecipeCraft> allRecipes;

    public bool CraftItem(RecipeCraft recipe)
    {
        // Periksa apakah pemain memiliki semua bahan dalam resep
        foreach (var ingredient in recipe.ingredients)
        {
            if (ingredient == null)
                continue;

            ItemSlot itemSlot = new ItemSlot { item = ingredient, count = 1 };

            if (!playerInventory.CheckItem(itemSlot))
                return false; // Pemain tidak memiliki bahan yang dibutuhkan
        }

        // Hapus bahan dari inventaris pemain
        foreach (var ingredient in recipe.ingredients)
        {
            if (ingredient == null)
                continue;
            playerInventory.Remove(ingredient);
        }

        // Tambahkan item hasil ke inventaris pemain
        playerInventory.Add(recipe.result);

        return true;
    }
    public RecipeCraft CheckRecipe(Item[,] craftingGrid)
    {
        foreach (RecipeCraft recipe in allRecipes) // Anda perlu membuat daftar semua resep Anda di suatu tempat
        {
            if (recipe.MatchesGrid(craftingGrid))
            {
                return recipe;
            }
        }

        return null; // Jika tidak ada resep yang cocok, kembalikan null
    }

}
