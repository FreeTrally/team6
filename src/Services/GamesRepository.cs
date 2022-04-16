using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using thegame.Domain;

namespace thegame.Services
{
    public class GamesRepo
    {
        private readonly Dictionary<Guid, Game> games = new();

        private readonly Dictionary<Guid, Game> start = new();

        public Game? FindStartGame(Guid id)
        {
            return start[id];
        }

        public Game? FindGameById(Guid id)
        {
            return games[id];
        }

        public void ReplaceGame(Guid idToReplace, Game game)
        {
            games[idToReplace] = game.Clone();
        }

        public void SaveGame(Game game)
        {
            games[game.Id] = game;
            start[game.Id] = game.Clone();
        }
    }
}