using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using thegame.Domain;
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

            if (userInput.KeyPressed == 'R')
            {
                game = gamesRepo.FindStartGame(gameId);
                gamesRepo.ReplaceGame(gameId, game);
                return Ok(game.ToGameDto());
            }

            var move = Vector.FromChar((char) userInput.KeyPressed);
            game.Move(move);

            return Ok(game.ToGameDto());
        }
    }
}