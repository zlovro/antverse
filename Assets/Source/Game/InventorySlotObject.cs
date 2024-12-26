using Source.Libraries.KBLib2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Game
{
    public class InventorySlotObject : Kb2Behaviour
    {
        [HideInInspector]
        public CraftingRecipe.Ingredient ingredient;

        public TextMeshProUGUI labelName;
        public TextMeshProUGUI labelAmount;
        public Image           itemIcon;
        public Image           bg;

        public Color selectedColor, deselectedColor;

        [HideInInspector]
        public bool selected;

        private void Update()
        {
            var item = ItemRegistry.Items[ingredient.itemType];

            labelName.text   = item.name;
            labelAmount.text = $"{ingredient.amount}";
            itemIcon.sprite  = item.sprite;

            bg.color = selected ? selectedColor : deselectedColor;
        }

        public void ToggleSelected()
        {
            selected = !selected;
        }
    }
}