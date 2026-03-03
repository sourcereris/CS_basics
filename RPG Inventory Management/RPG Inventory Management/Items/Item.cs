using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Inventory_Management
{
    internal abstract class Item
    {
        public string Name { get; private set; }
        public int ID { get; private set; }
        public int Weight { get; private set; }

        public Item(string name, int id, int weight) 
        {
            Name = name;
            ID = id;
            Weight = weight;
        }
    }
}
