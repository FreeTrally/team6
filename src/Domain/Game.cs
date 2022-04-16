using System;
using System.Collections.Generic;
using System.Linq;
using thegame.Models;

namespace thegame.Domain
{
    public class Cell
    {
        public Cell(int id, CellType type)
        {
            Id = id;
            Type = type;
        }

        public int Id { get; set; }
        public CellType Type { get; set; }
    }

    //public class Target
    //{
    //    public Target(int id, Vector position)
    //    {
    //        Id = id;
    //        Position = position;
    //    }

    //    public int Id { get; set; }
    //    public Vector Position { get; set; }
    //}

    public class Game
    {
        public string Solve { get; set; }
        public Guid Id { get; }
        public Cell[,] Field { get; }
        public Dictionary<Vector, int> Targets { get; } = new Dictionary<Vector, int>();

        private Vector playerPos;

        public Game(Guid id, CellType[,] field)
        {
            Id = id;
            var width = field.GetLength(0);
            var height = field.GetLength(1);
            Field = new Cell[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var cellId = x * height + y;
                    var type = field[x, y];
                    Field[x, y] = new Cell(cellId, type);
                    if (type == CellType.Player)
                        playerPos = new Vector(x, y);
                    else if (type is CellType.Target or CellType.BoxOnTarget)
                        Targets.Add(new Vector(x, y), -cellId);
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
                    var cell = Field[x, y];
                    var pos = new Vector(x, y);
                    if (Targets.ContainsKey(pos))
                    {                      
                        switch (cell.Type)
                        {
                            case CellType.Empty:
                                //cell.Type = CellType.Target;
                                cells.Add(new CellDto(Targets[pos].ToString(), new VectorDto(x, y), CellType.Target.ToCssClass(), "", 1));
                                break;
                            case CellType.Box:
                                cell.Type = CellType.BoxOnTarget;
                                break;
                        }                       
                    }

                    cells.Add(new CellDto(cell.Id.ToString(), new VectorDto(x, y), cell.Type.ToCssClass(), "", 0));
                }
            }

            var score = GetScore();
            var isWin = score == Targets.Count;
            return new GameDto(cells.ToArray(), true, false, width, height, Id, isWin, score);
        }

        public void Move(Vector move)
        {
            var current = playerPos;
            var currentCell = Field[current.X, current.Y];
            var nextPos = playerPos + move;
            var nextCell = Field[nextPos.X, nextPos.Y];

            if (nextCell.Type is CellType.Wall)
                return;

            if (nextCell.Type is CellType.Empty or CellType.Target)
            {
                Field[current.X, current.Y] = new Cell(nextCell.Id, CellType.Empty);
                Field[nextPos.X, nextPos.Y] = new Cell(currentCell.Id, CellType.Player);
                playerPos = nextPos;
                return;
            }

            if (nextCell.Type is CellType.Box or CellType.BoxOnTarget)
            {
                var nextNextPos = nextPos + move;
                var nextNextCell = Field[nextNextPos.X, nextNextPos.Y];

                if (nextNextCell.Type is CellType.Wall or CellType.Box)
                    return;

                if (nextNextCell.Type is CellType.Empty or CellType.Target)
                {
                    Field[current.X, current.Y] = new Cell(nextNextCell.Id, CellType.Empty);
                    Field[nextPos.X, nextPos.Y] = new Cell(currentCell.Id, CellType.Player);
                    playerPos = nextPos;

                    Field[nextNextPos.X, nextNextPos.Y] = new Cell(nextCell.Id, CellType.Box);
                }
            }
        }

        public int GetScore()
        {
            return Targets.Count(vec => Field[vec.Key.X, vec.Key.Y].Type is CellType.Box or CellType.BoxOnTarget);
        }

        public Game Clone()
        {
            var clone = Field.Clone();
            var width = Field.GetLength(0);
            var height = Field.GetLength(1);
            var fieldClone = new CellType[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    fieldClone[x,y] = Field[x, y].Type;                    
                }
            }
            var ng = new Game(Id, fieldClone);
            ng.Solve = Solve;
            return ng;
        }
    }
}