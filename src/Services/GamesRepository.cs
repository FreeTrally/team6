using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Domain;

namespace thegame.Services
{
    public class GamesRepo
    {
        private readonly Dictionary<Guid, Game> games = new();

        public Game? FindGameById(Guid id)
        {
            return games[id];
        }

        public void SaveGame(Game game)
        {
            games[game.Id] = game;
        }
    }
}