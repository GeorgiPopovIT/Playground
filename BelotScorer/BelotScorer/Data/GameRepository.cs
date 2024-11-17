using BelotScorer.Common;
using BelotScorer.Models;
using SQLite;

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
                await this._database.CreateTableAsync<Point>();
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

            var gameId = await this._database.InsertAsync(game);
            return game;
        }

        public async Task SavePointToTeam(int gameId, int teamScore, string teamName, int pointToAdd)
        {
            var expression = teamScore == 0 ? $"{teamScore}-{pointToAdd}" : $"{pointToAdd}-{teamScore}";
            var point = new Point
            {
                TeamName = teamName,
                GameId = gameId,
                Value = expression
            };

            await this.AddPoints(point);
        }
        public async Task SavePointsToTeams(Game currentGame, int team1PointToAdd, int team2PointToAdd)
        {

        }

        public async Task AddPointToEndScore(Game currentGame, int team1PointToAdd, int team2PointToAdd)
        {
            currentGame.Team1Score += team1PointToAdd;
            currentGame.Team2Score += team2PointToAdd;

            if (currentGame.Team1Score >= Constants.END_GAME_POINT ||
                currentGame.Team2Score >= Constants.END_GAME_POINT)
            {
                currentGame.IsGameFinished = true;
            }
            else
            {
                currentGame.IsGameFinished = false;
            }

            await this._database.UpdateAsync(currentGame);
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
                .OrderByDescending(g => g.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateGameAsync(Game item)
        {
            await Init();
            return await _database.UpdateAsync(item);
        }

        public async Task<int> DeleteGameAsync(Game game)
        {
            await Init();
            return await _database.DeleteAsync<Game>(game.Id);
        }

        public async Task<IEnumerable<Point>> GetPointsForTeam(string teamName, int gameId)
        {
            await Init();

            return await this._database.Table<Point>()
                .Where(p => p.GameId == gameId
                && p.TeamName == teamName)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task AddPoints(params Point[] points)
        {
            await Init();
            await this._database.InsertAllAsync(points);
        }

        public async Task DeleteAllPoints()
        {
            await Init();
            await this._database.DeleteAllAsync<Point>();
        }
    }
}
