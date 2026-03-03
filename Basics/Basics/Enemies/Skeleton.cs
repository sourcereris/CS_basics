using System;
using System.Collections.Generic;
using System.Text;

namespace Basics
{
    public class Skeleton : Enemy
    {
        public Skeleton() : base() { }
        public Skeleton(float hp) : base(hp) { }

        string name = "Goblin";
        public override void Attack()
        {
            Console.WriteLine("Shoot Arrow!");
        }
        public override void TakeDamage(float damage)
        {
            /*
             * if(DamageType.Arrow) damage *= 0.5f;
             * else if(DamageType.Sword) damage *= 1.5f;
             */

            if (damage <= 10) return; // Ignore small damage

            base.TakeDamage(damage);
            Console.WriteLine($"{name} took {damage} damage, remaining health: {Health}");
        }
    }
}
