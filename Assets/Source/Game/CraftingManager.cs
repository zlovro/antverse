using System;
using System.Collections.Generic;
using System.Linq;
using Source.Libraries.KBLib2;
using UnityEngine;

namespace Source.Game
{
    public class CraftingManager : Kb2Behaviour
    {
        public Transform  itemParent;
        public GameObject itemPfb;
        public Transform  inventorySlotParent;

        private Inventory mInventory;

        private void Start()
        {
            mInventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var inventorySlotObjects = inventorySlotParent.GetComponentsInChildren<InventorySlotObject>().Where(p => p.selected).ToList();
                inventorySlotObjects.ForEach(p => p.selected = false);

                var used = new HashSet<Item.ItemType>();
                foreach (var recipe in CraftingRecipeRegistry.Recipes)
                {
                    var matches   = true;
                    var amountMap = new List<Tuple<Item.ItemType, int>>();

                    foreach (var reqIng in recipe.ingredients)
                    {
                        var found = false;
                        foreach (var pair in inventorySlotObjects)
                        {
                            var type   = pair.ingredient.itemType;
                            var amount = mInventory.Items[type];
                            // var providedIng = slot.ingredient;
                            if (used.Contains(type))
                            {
                                continue;
                            }

                            if (reqIng.itemType == type && amount >= reqIng.amount)
                            {
                                found = true;
                                amountMap.Add(new Tuple<Item.ItemType, int>(type, reqIng.amount));
                                break;
                            }
                        }

                        if (!found)
                        {
                            matches = false;
                            break;
                        }
                    }

                    if (matches)
                    {
                        amountMap.ForEach(p =>
                        {
                            var type = p.Item1;
                            mInventory.Items[type] -= p.Item2;
                            used.Add(type);
                        });

                        var itemObj = Instantiate(itemPfb, itemParent);
                        itemObj.transform.position = tf.position;

                        var item = itemObj.GetComponent<ItemObject>();
                        item.type = recipe.result;

                        mInventory.Close();
                        
                        break;
                    }
                }
            }
        }
    }
}