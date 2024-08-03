using BelotScorer.Common;
using BelotScorer.Models;
using SQLite;
using static BelotScorer.Common.Constants;

namespace BelotScorer.Data
{
    public class GameRepository
    {
        SQLiteAsyncConnection _database;

        public GameRepository()
        { }


        async Task Init()
        {
            if (this._database is not null)
            {
                return;
            }

            try
            {
                this._database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                await this._database.CreateTableAsync<Game>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Game> CreateGame(string team1Name, string team2Name)
        {
            await Init();

            var game = new Game
            {
                Team1Name = team1Name,
                Team2Name = team2Name
            };

            await this._database.InsertAsync(game);

            return game;
        }

        public void SavePointsToTeams(Game game, int team1PointToAdd, int team2PointToAdd)
        {
            var currentGame = this.GetGameAsync(game.Id).Result;
            //var currentGame = game;

            currentGame.Team1Points.Add(team1PointToAdd);
            currentGame.Team2Points.Add(team2PointToAdd);

            currentGame.Team1Score += team1PointToAdd;
            currentGame.Team2Score += team2PointToAdd;

            if (currentGame.Team1FinalPoints >= Constants.END_GAME_POINT ||
                currentGame.Team2FinalPoints >= Constants.END_GAME_POINT)
            {
                currentGame.IsGameFinished = true;
            }
            else
            {
                currentGame.IsGameFinished = false;
            }
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            await Init();
            return await _database.Table<Game>().ToListAsync();
        }

        public async Task<Game> GetGameAsync(int id)
        {
            await Init();
            return await _database.Table<Game>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Game> GetLastGameAsync()
        {
            await Init();

            return await _database.Table<Game>()
                .OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateGameAsync(Game item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }

        public async Task<int> DeleteGameAsync(Game item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }

    }
}
