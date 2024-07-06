namespace ConsoleApp3.MongoDbTest;

public static class CustometDatabaseSettings
{
    public static string ConnectionString { get; set; } = "mongodb://localhost:27017";

    public static string DatabaseName { get; set; } = "Customers";

    //public static string BooksCollectionName { get; set; } = "CustomerData";
}
