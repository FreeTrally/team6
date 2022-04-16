using System;

namespace thegame.Domain
{
    public static class Extensions
    {
        public static string ToCssClass(this CellType cellType)
        {
            return cellType switch
            {
                CellType.Empty => "",
                CellType.Player => "player",
                CellType.Box => "box",
                CellType.BoxOnTarget => "boxOnTarget",
                CellType.Wall => "wall",
                CellType.Target => "target",
                _ => throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null)
            };
        }
    }
}