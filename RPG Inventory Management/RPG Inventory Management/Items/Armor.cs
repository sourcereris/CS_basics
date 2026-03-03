using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Inventory_Management
{
    internal class Armor : Item
    {
        public int DefenceRating { get; private set; }

        public Armor(string name, int id, int weight, int defenceRating) : base(name, id, weight)
        {
            DefenceRating = defenceRating;
        }
    }
}
