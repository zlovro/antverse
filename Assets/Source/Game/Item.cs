using System;
using System.Collections.Generic;
using Source.Libraries.KBLib2;
using UnityEngine;

namespace Source.Game
{
    [Serializable]
    public class Item
    {
        public enum ItemType
        {
            ITEM_GOLD,
            ITEM_IRON,
            ITEM_STONE,
            ITEM_STRING,
            ITEM_WOOD,
            
            ITEM_GOLD_ROCK,
            ITEM_IRON_ROCK,
            ITEM_ROCK,
            ITEM_TREE,

            TRAP_LANDMINE,
            TRAP_TRIPWIRE,
            TRAP_BEARTRAP
        }

        public ItemType id;
        public string   name;
        public Sprite   sprite;
    }
}