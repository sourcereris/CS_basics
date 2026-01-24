using System;
using System.Collections.Generic;
using System.Text;

namespace Basics
{
    internal class PlayerManager
    {
        public PlayerManager() 
        {
            Enemy.OnDeath += GiveXP;

            List<IDamageable> damageables = new List<IDamageable>
            {
                new Skeleton(),
                new Crate()
            };

            foreach (var damageable in damageables)
            {
                damageable.TakeDamage(20);
                damageable.TakeDamage(90);
                damageable.TakeDamage(15);
            }
        }


        void GiveXP(string s) 
        {
            Console.WriteLine($"{s} gained XP!");
        }
    }
}
