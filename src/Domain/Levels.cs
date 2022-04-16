using System;

namespace thegame.Domain
{
    public static class Levels
    {
        private static string level1 = @"WWWWW
WPEEW
WEBEW
WEETW
WWWWW";
        private static string level2 = @"EEWWWWWE
WWWEEEWE
WTPBEEWE
WWWEBTWE
WTWWBEWE
WEWETEWW
WBENBBTW
WEEETEEW
WWWWWWWW";
        private static string level3 = @"WWWWW
WPEEW
WEBEW
WEETW
WEBEW
WEETW
WWWWW";

        public static CellType[,] FromInt(int lvl)
        {
            return lvl switch
            {
                1 => ParseFromString(level1),
                2 => ParseFromString(level2),
                3 => ParseFromString(level3),
                _ => throw new ArgumentOutOfRangeException(nameof(lvl), lvl, null)
            };
        }

        public static string SolveFromInt(int lvl)
        {
            return lvl switch
            {
                1 => "RSDWDS",
                2 => "RDWDDSSSSASDAASAAWWSSDDWDDWWWWAAADSDSDSSAASAAWD",
                3 => "RSSSDWDSWWWASASD",
                _ => ""
            };
        }

        public static CellType[,] ParseFromString(string str) => ParseFromLines(str.Split('\n'));

        public static CellType[,] ParseFromLines(string[] lines)
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