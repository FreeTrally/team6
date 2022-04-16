namespace thegame.Domain
{
    public record Vector(int X, int Y)
    {
        public static Vector FromChar(char input)
        {
            return input switch
            {
                'W' => new Vector(0, -1),
                'S' => new Vector(0, 1),
                'D' => new Vector(1, 0),
                'A' => new Vector(-1, 0),
                _ => new Vector(0, 0)
            };
        }

        public static Vector operator +(Vector a, Vector b)
            => new(a.X + b.X, a.Y + b.Y);
    }
}