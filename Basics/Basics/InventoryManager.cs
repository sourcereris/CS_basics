using System;
using System.Collections.Generic;
using System.Text;

namespace Basics
{
    public class InventoryManager
    {
        // Make the dictionary private so it can only be modified via your methods
        private Dictionary<string, int> inventory;

        public IReadOnlyDictionary<string, int> Inventory => inventory; // Expose the inventory as a read-only property
        // Constructor needs to be public so you can create new instances
        public InventoryManager()
        {
            inventory = new Dictionary<string, int>();
        }

        public void AddItem(string item, int quantity)
        {
            if (inventory.TryGetValue(item, out int currentQuantity))
            {
                // Item exists, add to the stack
                inventory[item] = currentQuantity + quantity;
            }
            else
            {
                // New item, add it to the dictionary
                inventory.Add(item, quantity);
            }
        }

        public void CombineInventory(InventoryManager other)
        {
            foreach (var item in other.Inventory)
            {
                // Add to the current inventory
                AddItem(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Removes one unit of the specified item from the inventory.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(string item) 
        {
            if (inventory.TryGetValue(item, out int currentQuantity)) 
            {
                if(currentQuantity <= 0)
                {
                    throw new InvalidOperationException($"Cannot remove item '{item}' because its quantity is already zero.");
                }

                inventory[item] = currentQuantity - 1;

                if (inventory[item] <= 0)
                {
                    inventory.Remove(item);
                }
            }
        }
    }
}
