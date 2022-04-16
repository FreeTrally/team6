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

        public Game(Guid id, CellType[,] field)
        {
            Field = field;
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

            return new GameDto(cells.ToArray(), true, false, width, height, Guid.Empty, false, 0);
        }

        public int GetScore()
        {
            return 0;
        }
    }
}