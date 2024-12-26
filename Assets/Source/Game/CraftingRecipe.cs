using System;

namespace Source.Game
{
    [Serializable]
    public class CraftingRecipe
    {
        [Serializable]
        public class Ingredient
        {
            public Item.ItemType itemType;
            public int           amount;
        }
        public Ingredient[]  ingredients;
        public Item.ItemType result;
    }
}