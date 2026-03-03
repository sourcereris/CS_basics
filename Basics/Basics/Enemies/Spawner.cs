using System;
using System.Collections.Generic;
using System.Text;

namespace Basics
{
    internal class Spawner<T> where T : Enemy, new()
    {
        public T Spawn()
        {
            T enemy = new T();
            Console.WriteLine($"Spawns {typeof(T).Name}");
            return enemy;
        }
    }
}
