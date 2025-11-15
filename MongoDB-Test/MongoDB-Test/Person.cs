using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_Test;

public class Person
{
    [BsonId]
    public ObjectId _id { get; set; }

    [BsonElement("first_name")]
    public string FirstName { get; set; }

    [BsonElement("last_name")]
    public string LastName { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }
}
