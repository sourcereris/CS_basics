using System;
using System.Collections.Generic;

Dictionary<string, string> dictionary = new()
{
    { "Hello", "Labas" },
    { "Goodbye", "Viso gero" },
    { "Thank you", "Ačiū" },
    { "Please", "Prašau" },
    { "Yes", "Taip" },
    { "No", "Ne" },
    { "Friend", "Draugas" },
    { "Bread", "Duona" },
    { "Water", "Vanduo" },
    { "Sun", "Saulė" }
};

Console.WriteLine(GetTranslation("Hello"));
Console.WriteLine(GetTranslation("Goodbye"));
Console.WriteLine(GetTranslation("Thanks"));

string GetTranslation(string englishKey) 
{
    if (dictionary.TryGetValue(englishKey, out string? lithuanianTranslation))
    {
        return lithuanianTranslation;
    }
    
    return $"[{englishKey}]";
}

// ==========================================
// Task 2: The Crafting Validator
// ==========================================

Dictionary<string, int> recipe = new Dictionary<string, int>()
{
    { "Iron Ingot", 2 },
    { "Wood", 1 },
    { "Leather Strap", 1 }
};

Dictionary<string, int> playerInventory = new Dictionary<string, int>()
{
    { "Iron Ingot", 5 },
    { "Wood", 10 }
    // Leather Strap is missing completely!
};

bool canCraftSword = CanCraft(playerInventory, recipe);
Console.WriteLine($"\nCan craft Steel Sword? {canCraftSword}");

bool CanCraft(Dictionary<string, int> inventory, Dictionary<string, int> recipeCost)
{
    foreach (var item in recipeCost) 
    {
        if (inventory.TryGetValue(item.Key, out int available)) 
        {
            if (item.Value > available) 
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    return true;
}