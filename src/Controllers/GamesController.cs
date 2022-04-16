using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Domain;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamesRepo gamesRepo;

        public GamesController(GamesRepo gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }

        [Route("api/games/{id:guid}")]
        [HttpGet]
        public ActionResult<GameDto> GetGame(Guid id)
        {
            var game = gamesRepo.FindGameById(id);
            if (game == null)
                return NotFound();

            return game.ToGameDto();
        }

        [Route("api/games/{level:int}")]
        [HttpPost]
        public ActionResult<GameDto> Index(int level, [FromQuery] Guid? id)
        {
            var game = new Game(id ?? Guid.NewGuid(), Levels.FromInt(level));
            game.Solve = Levels.SolveFromInt(level);
            gamesRepo.SaveGame(game);
            return Ok(game.ToGameDto());
        }
    }
}