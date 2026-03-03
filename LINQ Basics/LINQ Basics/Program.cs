using System;
using System.Collections.Generic;
using System.Linq;

public class Unit
{
    public string Name { get; set; }
    public string Role { get; set; } // "Tank", "DPS", "Healer"
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Initiative { get; set; }
}

public class Program
{
    public static void Main()
    {
        List<Unit> party = new List<Unit>
        {
            new Unit { Name = "Alistair", Role = "Tank", Health = 120, MaxHealth = 120, Initiative = 15 },
            new Unit { Name = "Morrigan", Role = "DPS", Health = 0, MaxHealth = 80, Initiative = 45 },
            new Unit { Name = "Leliana", Role = "DPS", Health = 40, MaxHealth = 75, Initiative = 60 },
            new Unit { Name = "Wynne", Role = "Healer", Health = 60, MaxHealth = 60, Initiative = 30 }
        };

        var aliveUnits = party.Where(u => u.Health > 0);
        var sortedByInitiative = aliveUnits.OrderByDescending(u => u.Initiative);
        bool healthyUnit = sortedByInitiative.Any(u => u.Health < u.MaxHealth / 2.0f);
        var healer = aliveUnits.FirstOrDefault(u => u.Role == "Healer");

        List<string> nameList = party.Select(u => u.Name).ToList();
    }
}