using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp3.MongoDbTest;

public class Customer
{
    public Customer()
    {
        this.Id = ObjectId.GenerateNewId();
    }

    //[BsonId]
    public ObjectId? Id { get; set; }
  
    public string? Name { get; set; }

    public string? Order { get; set; }

    public Address Address { get; set; }
}
