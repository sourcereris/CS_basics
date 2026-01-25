using System.Collections.Generic;
using Basics.Math;
using Basics;

Vector2 Player = new Vector2 { X = 10, Y = 5 };
Vector2 Enemy = new Vector2 { X = 2, Y = 2 };

Vector2 Direction = Player - Enemy;
Direction = Direction.Normalized;


PlayerManager playerManager = new PlayerManager();

Spawner<Skeleton> skeletonSpawner = new Spawner<Skeleton>();

List<Enemy> enemies = new List<Enemy>();

enemies.Add(new Skeleton(15));
enemies.Add(new Skeleton(35));
enemies.Add(new Skeleton(100));
enemies.Add(new Skeleton(100));
enemies.Add(new Skeleton(25));


List<Enemy> exequteTargets = enemies.Where(e => e.Health <= 30).ToList();

foreach (var enemy in exequteTargets)
{
    enemy.TakeDamage(9999);
}

Dictionary<string, int> Ammo = new Dictionary<string, int>();
string Bang = "Bang!";
string Click = "Click";
string no = "... ";
string ammo = "(Empty)";

Ammo.Add("Arrow", 50);
Ammo.Add("Bullet", 20);
Ammo.Add("Rocket", 5);

FireWeapon("Arrow");
Ammo["Arrow"] = 0;
FireWeapon("Arrow");

void FireWeapon(string weaponType)
{
    if (Ammo.ContainsKey(weaponType) && Ammo[weaponType] > 0 )
    {
        Ammo[weaponType]--;
        Console.WriteLine(Bang);
    }
    else
    {
        Console.WriteLine(Click + no + ammo);
    }
}

List<Skeleton> skeletons = new List<Skeleton>();
for (int i = 0; i < 3; i++)
{
    Skeleton skeleton = skeletonSpawner.Spawn();
    skeletons.Add(skeleton);
}

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