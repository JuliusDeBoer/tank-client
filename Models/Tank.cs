﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tank_client.Models
{
    public class TankTotal
    {
        public int Total { get; set; }
        public List<Tank> Tanks { get; set; } = new List<Tank>();

        public TankTotal(Dictionary<int, Tank> tanks)
        {
            foreach (KeyValuePair<int, Tank> pair in tanks)
            {
                if (pair.Value.Health >= 1)
                {
                    Tanks.Add(pair.Value);
                    Total++;
                }
            }
        }
    }

    public enum Color
    {
        Red = 0,
        Orange = 1,
        Yellow = 2,
        Green = 3,
        Blue = 4,
        Purple = 5,
        White = 6,
        Hotpink = 7 // Really cool and secret!
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Tanks
    {
        public int Total { get; set; }
        public Dictionary<int, Tank> AllTanks { get; set; }
    }

    public class Tank
    {
        public int Id { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int ActionPoints { get; set; }
        public Color Color { get; set; }
        public Position Position { get; set; } = new(0, 0);

        public Tank(int id)
        {
            Id = id;
        }
    }
}