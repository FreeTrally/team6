using System.Linq;

namespace thegame.Domain
{
    public static class Levels
    {
        public static CellType[,] Level1()
        {
            return new CellType[,]
            {
                {CellType.Box, CellType.Box, CellType.Box},
                {CellType.Box, CellType.Box, CellType.Box},
                {CellType.Box, CellType.Box, CellType.Box},
                {CellType.Box, CellType.Box, CellType.Box},
            };
        }

        public static CellType[,] ParseFromString(string[] lines)
        {
            var cells = new CellType[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    cells[x, y] = SwitchFromChar(lines[y][x]);
                }
            }

            return cells;
        }

        public static CellType SwitchFromChar(char c)
        {
            switch (c)
            {
                case 'W': return CellType.Wall;
                case 'E': return CellType.Empty;
                case 'P': return CellType.Player;
                case 'T': return CellType.Target;
                case 'N': return CellType.BoxOnTarget;
                case 'B': return CellType.Box;
                default: return CellType.Empty;
            }
        }
    }
}