using System.Collections.Generic;
using Source.Libraries.KBLib2;

namespace Source.Game
{
    public class ItemRegistry : Kb2Behaviour
    {
        public static Dictionary<Item.ItemType, Item> Items = new ();

        public Item[] items;

        protected override void Awake()
        {
            base.Awake();

            foreach (var item in items)
            {
                Items[item.id] = item;
            }
        }
    }
}