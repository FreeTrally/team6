using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games/{gameId}/moves")]
    public class MovesController : Controller
    {
        private readonly GamesRepo gamesRepo;

        public MovesController(GamesRepo gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }

        [HttpPost]
        public IActionResult Moves(Guid gameId, [FromBody] UserInputDto userInput)
        {
            var game = gamesRepo.FindGameById(gameId);
            if (game == null)
                return NotFound();

            if (userInput.ClickedPos != null)
                game.Cells.First(c => c.Type == "player").Pos = userInput.ClickedPos;

            if (userInput.KeyPressed != 0)
            {
                switch ((char)userInput.KeyPressed)
                {
                    case 'W':
                        {
                            var pos = game.Cells.First(c => c.Type == "player").Pos;
                            var newPos = new VectorDto(pos.X, pos.Y + 1);
                            game.Cells.First(c => c.Type == "player").Pos = newPos;
                            break;
                        }
                    case 'D':
                        {
                            var pos = game.Cells.First(c => c.Type == "player").Pos;
                            var newPos = new VectorDto(pos.X + 1, pos.Y);
                            game.Cells.First(c => c.Type == "player").Pos = newPos;
                            break;
                        }
                    case 'A':
                        {
                            var pos = game.Cells.First(c => c.Type == "player").Pos;
                            var newPos = new VectorDto(pos.X - 1, pos.Y);
                            game.Cells.First(c => c.Type == "player").Pos = newPos;
                            break;
                        }
                    case 'S':
                        {
                            var pos = game.Cells.First(c => c.Type == "player").Pos;
                            var newPos = new VectorDto(pos.X, pos.Y - 1);
                            game.Cells.First(c => c.Type == "player").Pos = newPos;
                            break;
                        }
                }
            }

            return Ok(game.ToGameDto());
        }
    }
}