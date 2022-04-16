using System;
using System.Collections.Generic;
using thegame.Models;

namespace thegame.Domain
{
    public class Game
    {
        public Guid Id { get; }
        public CellType[,] Field { get; }
        public List<Vector> Targets { get; }

        private Vector playerPos;

        public Game(Guid id, CellType[,] field)
        {
            Field = field;
            var width = Field.GetLength(0);
            var height = Field.GetLength(1);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (field[x, y] == CellType.Player)
                    {
                        playerPos = new Vector(x, y);
                        break;
                    }
                }
            }
        }

        public GameDto ToGameDto()
        {
            var cells = new List<CellDto>();
            var width = Field.GetLength(0);
            var height = Field.GetLength(1);
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var id = x * height + y;
                    cells.Add(new CellDto(id.ToString(), new VectorDto(x, y), Field[x, y].ToString(), "", 0));
                }
            }

            return new GameDto(cells.ToArray(), true, false, width, height, Id, false, 0);
        }

        public void Move(Vector move)
        {
            var current = playerPos;
            var nextPos = playerPos + move;
            var nextCell = Field[nextPos.X, nextPos.Y];

            if (nextCell is CellType.Wall)
                return;

            if (nextCell is CellType.Empty or CellType.Target)
            {
                Field[current.X, current.Y] = CellType.Empty;
                Field[nextPos.X, nextPos.Y] = CellType.Player;
                playerPos = nextPos;
                return;
            }

            if (nextCell is CellType.Box)
            {
                var nextNextPos = nextPos + move;
                var nextNextCell = Field[nextNextPos.X, nextNextPos.Y];

                if (nextNextCell is CellType.Wall or CellType.Box)
                    return;

                if (nextNextCell is CellType.Empty or CellType.Target)
                {
                    Field[current.X, current.Y] = CellType.Empty;
                    Field[nextPos.X, nextPos.Y] = CellType.Player;
                    playerPos = nextPos;

                    Field[nextNextPos.X, nextNextPos.Y] = CellType.Box;
                }
            }
        }

        public int GetScore()
        {
            return 0;
        }
    }
}