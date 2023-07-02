﻿using BelotScorer.Common;
using BelotScorer.Models;
using SQLite;

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

        public async Task<List<Game>> GetGameAsync()
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