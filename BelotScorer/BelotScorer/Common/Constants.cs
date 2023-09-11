namespace BelotScorer.Common
{
    internal static class Constants
    {
        internal const string DATABASE_FILE_NAME = "BelotScorerSQLite.db3";

        internal const short END_GAME_POINT = 151;
        internal const short TEAM_NAME_MAX_LENGTH = 30;


        internal static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DATABASE_FILE_NAME);

    }
}
