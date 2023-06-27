namespace BelotScorer.Common
{
    internal static class Constants
    {
        public const string DatabaseFilename = "BelotScorerSQLite.db3";

        public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    }
}
