using System;
using Microsoft.AspNetCore.Mvc;
using thegame.Domain;
using thegame.Models;
using thegame.Services;

namespace thegame.Controllers
{
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly GamesRepo gamesRepo;

        public GamesController(GamesRepo gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }

        [HttpPost]
        public ActionResult<GameDto> Index()
        {
            var game = new Game(Guid.NewGuid(), Levels.Level1());
            gamesRepo.SaveGame(game);
            return Ok(game.ToGameDto());
        }
    }
}