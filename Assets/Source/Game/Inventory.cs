using System;
using System.Collections.Generic;
using Source.Libraries.KBLib2;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Game
{
    public class Inventory : Kb2Behaviour
    {
        public GameObject inventoryPanel;
        public GameObject slotPfb;

        public Dictionary<Item.ItemType, int> Items = new();

        public bool inventoryOpened;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventoryOpened)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }

            foreach (var slot in inventoryPanel.GetComponentsInChildren<InventorySlotObject>())
            {
                var type = slot.ingredient.itemType;
                if (!Items.TryGetValue(type, out var item))
                {
                    DestroyImmediate(slot.gameObject);
                    continue;
                }

                slot.ingredient.amount = item;
            }

            inventoryPanel.SetActive(inventoryOpened);
        }

        public void Close()
        {
            inventoryOpened = false;
            
            foreach (Transform child in inventoryPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void Open()
        {
            inventoryOpened = true;
            
            foreach (var item in Items)
            {
                var slotGameObject = Instantiate(slotPfb, inventoryPanel.transform);

                var slot = slotGameObject.GetComponent<InventorySlotObject>();
                slot.ingredient.amount   = item.Value;
                slot.ingredient.itemType = item.Key;
            }
        }

        public void AddItem(Item.ItemType pItem, int pAmount)
        {
            Items.TryAdd(pItem, 0);
            Items[pItem] += pAmount;
        }

        public void UseItem(Item.ItemType pItem, int pAmount)
        {
            if (Items.ContainsKey(pItem))
            {
                Items[pItem] -= pAmount;
            }
        }
    }
}