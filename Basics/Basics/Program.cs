using Basics;
using System;
using System.Collections.Generic;

//Player p1 = new Player("Alice", 100, 20);
//p1.TakeDamage(DamageCalculator.CalculateDamage(50, p1.Armour));

Dictionary<string, int> inventory = new();

AddItem("Sword", 1);
AddItem("Shield", 1);
AddItem("Coin", 100);
AddItem("Coin", 50);

PrintInventory();


// To simplyfy we will pretend that we have a data base of items in a txt file or smth
// and we will use that txt file as a reference to fill our InvenotryDatabase.itemWeights Dictioary.
// also we will use the same txt for filling our player inventory.
InventoryManager playerInventory = new InventoryManager();

playerInventory.AddItem("Potion", 5);
playerInventory.AddItem("Rock", 3);

float InventoryWeight()
{
    float sum = 0;
    foreach(var item in playerInventory.Inventory)
    {
        ItemDatabase.ItemWeights.TryGetValue(item.Key, out float weight);
        sum += weight * item.Value;
    }

    return sum;
}

void AddItem(string itemName, int amount) 
{
    if (inventory.TryGetValue(itemName, out int currentAmount))
    {
        inventory[itemName] = currentAmount + amount;
    }
    else
    {
        inventory.Add(itemName, amount);
    }
}

void PrintInventory() 
{
    foreach (var item in inventory)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
}

class Player 
{
    private string _name;
    private int _hp;
    private int _armour;

    public Player(string name, int hp, int armour) 
    {
        _name = name;
        _hp = hp;
        _armour = armour;
    }

    // all methods below do the same thing, just different syntax for properties, right?

    //public int Armour() { return _armour; }
    //public int Armour => _armour;
    public int Armour {get { return _armour; } private set { } }
    public int HP { get => _hp; private set { } }
    public string Name { get => _name; private set { } }

    public void TakeDamage(int damage)
    {
        if (_hp > damage)
        {
            _hp -= damage;
            Console.WriteLine($"{_name} took {damage} damage and has {_hp} HP left.");
        }
        else
        {
            Console.WriteLine($"{_name} died");
            _hp = 0;
        }
    }
    public void SetArmour(int armour)
    {
        _armour = armour;
        Console.WriteLine($"{_name} now has {_armour} armour.");
    }
}

class DamageCalculator
{
    //staticm ethod used as utility for calculating damage after considering armour,
    //not only i player class but also other classes can use this method
    //without needing to create an instance of DamageCalculator
    public static int CalculateDamage(int baseDamage, int armour)
    {
        int damageAfterArmour = baseDamage - armour;
        return Math.Max(0, damageAfterArmour);
    }
}