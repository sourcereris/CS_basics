using System.Collections.Generic;
using Basics.Math;
using Basics;

Vector2 Player = new Vector2 { X = 10, Y = 5 };
Vector2 Enemy = new Vector2 { X = 2, Y = 2 };

Vector2 Direction = Player - Enemy;
Direction = Direction.Normalized;


PlayerManager playerManager = new PlayerManager();

public class Crate : IDamageable
{
    public float Health { get; set; } = 50;
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 250) Console.WriteLine("Crate cracks");
        else if(Health < 0) Health = 0;
    }
}