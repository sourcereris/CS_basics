using System;
using System.Collections.Generic;
using System.Text;

namespace Basics
{
    public abstract class Enemy : IDamageable
    {
        public float Health { get; set; } = 100;
        bool dead = false;
        public static event Action<string> OnDeath;
        public abstract void Attack();

        public virtual void TakeDamage(float damage)
        {
            
            if(Health != 0) Health -= damage;

            if (Health < 0 && !dead)
            {
                Health = 0;
                OnDeath?.Invoke("Enemy died");
                dead = true;
            }
        }
    }
}
