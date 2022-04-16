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
            var game = new Game(Guid.NewGuid(), level == 1 ? Levels.Level2() : Levels.Level3());
            gamesRepo.SaveGame(game);
            return Ok(game.ToGameDto());
        }
    }
}