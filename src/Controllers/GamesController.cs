using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Domain;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games/{level:int}")]
    public class GamesController : Controller
    {
        private readonly GamesRepo gamesRepo;

        public GamesController(GamesRepo gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }

        [HttpPost]
        public ActionResult<GameDto> Index(int level)
        {
            var game = new Game(Guid.NewGuid(), Levels.FromInt(level));
            game.Solve = Levels.SolveFromInt(level);
            gamesRepo.SaveGame(game);
            return Ok(game.ToGameDto());
        }
    }
}