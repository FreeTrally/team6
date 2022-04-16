using System;

namespace thegame.Domain
{
    public static class Levels
    {
        public static CellType[,] FromInt(int lvl)
        {
            return lvl switch
            {
                1 => Level1(),
                2 => Level2(),
                _ => throw new ArgumentOutOfRangeException(nameof(lvl), lvl, null)
            };
        }

        public static CellType[,] Level1()
        {
            var str = @"WWWWW
WPEEW
WEBEW
WEETW
WWWWW";
            return ParseFromString(str.Split('\n'));
        }

        public static CellType[,] Level2()
        {
            var str = @"EEWWWWWE
WWWEEEWE
WTPBEEWE
WWWEBTWE
WTWWBEWE
WEWETEWW
WBENBBTW
WEEETEEW
WWWWWWWW";
            return ParseFromString(str.Split('\n'));
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
            return c switch
            {
                'W' => CellType.Wall,
                'E' => CellType.Empty,
                'P' => CellType.Player,
                'T' => CellType.Target,
                'N' => CellType.BoxOnTarget,
                'B' => CellType.Box,
                _ => CellType.Empty,
            };
        }
    }
}