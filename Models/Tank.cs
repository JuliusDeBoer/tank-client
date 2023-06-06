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

    public class TankCollection
    {
        public int Total { get; set; }
        public Tank[] Tanks { get; set; }

        // Returns -1 if no tank was found
        public int GetTankByPos(int x, int y)
        {
            foreach (Tank tank in Tanks)
            {
                if(tank.Position.X == x
                    && tank.Position.Y == y)
                {
                    return tank.Id;
                }
            }

            return -1;
        }

        public Tank GetById(int id)
        {
            foreach(Tank tank in Tanks)
            {
                if(tank.Id == id) { return tank; }
            }

            throw new KeyNotFoundException();
        }
    }

    public class Tank
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int ActionPoints { get; set; }
        public Color Color { get; set; }
        public Position Position { get; set; } = new(0, 0);

        public Tank(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }
    }
}