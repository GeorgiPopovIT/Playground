using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp3.MongoDbTest;

public class Customer
{
    [BsonId]
    public string? Id { get; set; }

    [BsonElement("FirstName")]
    public string? Name { get; set; }   
}
