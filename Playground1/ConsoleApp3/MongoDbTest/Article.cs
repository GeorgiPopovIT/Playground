using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp3.MongoDbTest;

public class Article
{
    [BsonElement("author")]
    public string Author { get; set; }

    [BsonElement("date")]
    public string Date { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("rating")]
    public string Rating { get; set; }
}
