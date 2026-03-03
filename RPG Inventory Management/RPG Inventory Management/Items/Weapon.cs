using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Inventory_Management
{
    internal class Weapon : Item
    {
        public int AttackPower { get; private set; }

        public Weapon(string name, int id, int weight, int attackPower) : base(name, id, weight)
        {
            AttackPower = attackPower;
        }
    }
}
