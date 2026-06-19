using System;
using System.Collections.Generic;

Stack<string> UIScreens = new();

UIScreens.Push("Main Menu");
UIScreens.Push("Settings");
UIScreens.Push("Audio");

while(UIScreens.Count > 0)
{
    Console.WriteLine($"Closing Screen: {UIScreens.Pop()}");
}

List<Enemy> enemies = new List<Enemy>
{
    new Enemy(),
    new Goblin(),
    new Enemy(),
    new Goblin(),
    new Goblin()
};

foreach (var enemy in enemies) 
{
    enemy.Attack();
}

public class Enemy 
{
    public virtual void Attack()
    {
        Console.WriteLine("The enemy attacks!");
    }
}

public class Goblin : Enemy
{
    public override void Attack()
    {
        Console.WriteLine("The goblin swings a club!");
    }
}