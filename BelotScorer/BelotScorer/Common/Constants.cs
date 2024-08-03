namespace BelotScorer.Common
{
    internal static class Constants
    {
        internal const string DATABASE_FILE_NAME = "BelotScorerSQLite.db3";

        internal const short END_GAME_POINT = 151;
        internal const short TEAM_NAME_MAX_LENGTH = 30;

        internal const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        internal static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DATABASE_FILE_NAME);

    }
}
