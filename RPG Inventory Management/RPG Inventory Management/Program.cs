using RPG_Inventory_Management;

HashSet<int> uniqueID = new HashSet<int>();

Character Tony = new Character("Tony", 10, 5);
Character Orc = new Character("Orc", 12, 7);

Weapon sword = new Weapon("Sword", GetID(), 15, 15);
Weapon bow = new Weapon("Bow", GetID(), 13, 12);

Armor helmet = new Armor("Helmet", GetID(), 14, 8);
Armor shield = new Armor("Shield", GetID(), 17, 10);

Tony.PickUpItem(sword);
Tony.PickUpItem(helmet);

Orc.PickUpItem(bow);
Orc.PickUpItem(shield);


Console.WriteLine($"Tony's total attack: {Tony.GetTotalAttack()}");

Tony.PrintWeaponsOnly();
Console.WriteLine();

CombatSystem.ResolveAttack(Tony, sword, Orc, 0.9f, 2.0f);
int GetID()
{
    int id;
    do
    {
        id = Random.Shared.Next(1, 500);
    } while (uniqueID.Contains(id)); // Use .Contains() for O(1) lookup

    uniqueID.Add(id); // You must actually store the ID
    return id;
}