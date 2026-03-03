using System;
using System.Collections.Generic;
using System.Text;

namespace MVVM
{
    internal class PlayerModel
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public PlayerModel(string name, int health)
        {
            Name = name;
            Health = health;
        }
    }
}
