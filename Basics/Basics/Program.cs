using System;
using System.Collections.Generic;

Queue<string> players = new();

players.Enqueue("Human");
players.Enqueue("Orc");
players.Enqueue("Goblin");

while(players.Count > 0)
{
    Console.WriteLine($"{players.Dequeue()} turn");
}