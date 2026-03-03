using System;
using System.Collections.Generic;

namespace RPG_Inventory_Management
{
    internal class Character
    {
        List<Item> Inventory = new List<Item>();

        public string Name { get; private set; }
        public int Health { get; private set; } = 100;
        public int BaseAttack { get; private set; }
        public int BaseDefence { get; private set; }
        
        public Character(string name, int bAttack, int bDefence) 
        {
            Name = name;
            BaseAttack = bAttack;
            BaseDefence = bDefence;
        }

        public void TakeDamage(int amount) 
        {
            Health -= amount;
            Console.WriteLine($"{Name} takes {amount} damage! Current health: {Health}");
        }

        public void PickUpItem(Item item)
        {
            bool tooHeavy = Inventory.Sum(i => i.Weight) + item.Weight > 50 ;
            if (!tooHeavy)
            {
                Inventory.Add(item);
            }else Console.WriteLine("Cannot pick up item, inventory is too heavy!");

            Console.WriteLine($"Current inventory weight: {Inventory.Sum(i => i.Weight)}");
        }

        public void DropItem(int itemID)
        {
            var itemToRemove = Inventory.FirstOrDefault(i => i.ID == itemID);

            if (itemToRemove != null)
            {
                Inventory.Remove(itemToRemove);
            }
        }

        public void PrintWeaponsOnly() 
        {
            var weaponDetails = Inventory.OfType<Weapon>().Select(w => $"{w.Name} (Attack: {w.AttackPower})");
            Console.WriteLine("Weapons in inventory:\n- " + string.Join("\n- ", weaponDetails));
        }

        public int GetTotalAttack() 
        {
            return BaseAttack + Inventory.OfType<Weapon>().Sum(weapon => weapon.AttackPower);
        }

        public int GetTotalDefence()
        {
            return BaseDefence + Inventory.OfType<Armor>().Sum(armor => armor.DefenceRating);
            // or should i use (return BaseDefence + Inventory.Sum(item => item is Armor ? ((Armor)item).DefenceRating : 0);)?
        }
    }
}
