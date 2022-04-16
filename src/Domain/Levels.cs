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
    }
}