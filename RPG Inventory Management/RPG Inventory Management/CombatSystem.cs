using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Inventory_Management
{
    internal class CombatSystem
    {
        public static void ResolveAttack(Character attacker, Weapon attackerWeapon, Character defender, float critChance, float critMultiplier) 
        {
            float dRaw = attacker.GetTotalAttack();
            
            string attack = $"{attacker.Name} attacks!";
            float dTotal = dRaw;
            if (Random.Shared.NextDouble() < critChance)
            {
                dTotal *= critMultiplier;
                attack += " Critical hit!";
            }
            int dFinal = (int)Math.Round(dTotal * (100f/ (100f + defender.GetTotalDefence())));

            Console.WriteLine(attack);
            defender.TakeDamage(dFinal);
        }
    }
}
