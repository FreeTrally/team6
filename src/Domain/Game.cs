using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace thegame.Domain
{
    public class Game
    {
        public Guid Id { get; }
        public CellType[,] Field { get; }
        public List<Vector> Targets { get; } = new List<Vector>();

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
                    var cell = field[x, y];
                    if (cell == CellType.Player)
                        playerPos = new Vector(x, y);
                    else if (cell is CellType.Target or CellType.BoxOnTarget)
                        Targets.Add(new Vector(x, y));
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
                    var cellType = Field[x, y];
                    if (Targets.Contains(new Vector(x, y)))
                    {
                        switch (cellType)
                        {
                            case CellType.Empty:
                                cellType = CellType.Target;
                                break;
                            case CellType.Box:
                                cellType = CellType.BoxOnTarget;
                                break;
                        }
                    }

                    cells.Add(new CellDto(id.ToString(), new VectorDto(x, y), cellType.ToCssClass(), "", 0));
                }
            }

            var score = GetScore();
            var isWin = score == Targets.Count;
            return new GameDto(cells.ToArray(), true, false, width, height, Id, isWin, score);
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

            if (nextCell is CellType.Box or CellType.BoxOnTarget)
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
            return Targets.Count(vec => Field[vec.X, vec.Y] is CellType.Box or CellType.BoxOnTarget);
        }

        public Game Clone()
        {
            return new Game(Id, (CellType[,]) Field.Clone());
        }
    }
}