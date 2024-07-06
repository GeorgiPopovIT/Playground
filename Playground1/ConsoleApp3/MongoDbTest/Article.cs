using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp3.MongoDbTest;

public class Article
{
    public int Id { get; set; }
    [BsonElement("author")]
    public string Author { get; set; }

    [BsonElement("date")]
    public string Date { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("rating")]
    public string Rating { get; set; }

    public List<Animal> Animals { get; set; } = new List<Animal>();
}
