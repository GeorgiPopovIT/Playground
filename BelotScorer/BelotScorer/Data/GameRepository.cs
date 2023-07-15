﻿using BelotScorer.Common;
using BelotScorer.Models;
using SQLite;

using static BelotScorer.Common.Constants;

namespace BelotScorer.Data
{
    public class GameRepository
    {
        private SQLiteAsyncConnection _database;

        public GameRepository()
        {

        }

        async Task Init()
        {
            if (_database is not null)
                return;

            _database = new SQLiteAsyncConnection(Constants.DatabasePath);
            var result = await _database.CreateTableAsync<Game>();
        }

        public async Task CreateGame(string team1Name, string team2Name)
        {
            Init();

            await this._database.InsertAsync(new Game
            {
                Team1Name = team1Name,
                Team2Name = team2Name
            });

        }
        public async Task<bool> SavePointToTeam(int gameId, short team1PointToAdd, short team2PointToAdd)
        {
            var currentGame = await this.GetGameAsync(gameId);

            currentGame.Team1Points.Add(team1PointToAdd);
            currentGame.Team2Points.Add(team2PointToAdd);

            currentGame.Team1FinalPoints += team1PointToAdd;

            currentGame.Team2FinalPoints += team2PointToAdd;

            if (currentGame.Team1FinalPoints >= Constants.END_GAME_POINT ||
                currentGame.Team2FinalPoints >= Constants.END_GAME_POINT)
            {

                return true;
            }

            return false;

        }

        public async Task<List<Game>> GetGamesAsync()
        {
            await Init();
            return await _database.Table<Game>().ToListAsync();
        }

        public async Task<Game> GetGameAsync(int id)
        {
            await Init();
            return await _database.Table<Game>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveGameAsync(Game item)
        {
            await Init();
            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteGameAsync(Game item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
    }
}
