namespace BelotScorer.Common
{
    internal static class Constants
    {
        public const string DATABASE_FILE_NAME = "BelotScorerSQLite.db3";

        public const short END_GAME_POINT = 151;
        public const short TEAM_NAME_MAX_LENGTH = 30;


        public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DATABASE_FILE_NAME);

    }
}
