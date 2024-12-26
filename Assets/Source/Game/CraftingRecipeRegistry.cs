using System.Collections.Generic;
using Source.Libraries.KBLib2;

namespace Source.Game
{
    public class CraftingRecipeRegistry : Kb2Behaviour
    {
        public static List<CraftingRecipe> Recipes = new();

        public CraftingRecipe[] recipes;
        protected override void Awake()
        {
            base.Awake();
            
            Recipes.AddRange(recipes);
        }
    }
}