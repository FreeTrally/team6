namespace thegame.Domain
{
    public record Vector(int X, int Y)
    {
        public static Vector FromChar(char input)
        {
            switch (input)
            {
                case 'W':
                case 'w':
                {
                    return new Vector(0, -1);
                }
                case 'S':
                case 's':
                {
                    return new Vector(0, 1);
                }
                case 'D':
                case 'd':
                {
                    return new Vector(1, 0);
                }
                case 'A':
                case 'a':
                {
                    return new Vector(-1, 0);
                }
                default:
                {
                    return new Vector(0, 0);
                }
            }
        }

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a.X + b.X, a.Y + b.Y);
    }
}