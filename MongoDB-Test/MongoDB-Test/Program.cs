using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB_Test;

var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

string mongoConnectionString = config["MONGODB_URI"];

var client = new MongoClient(mongoConnectionString);
var db = PersonDbContext.Create(client.GetDatabase("cluster2"));

//await db.Persons.AddAsync(new() 
//{
//    FirstName = "John2",
//    LastName = "Doe2",
//    Email = "test2@gmail.com"
//});

//await db.SaveChangesAsync();


var p1 = db.Persons.FirstOrDefault(x => x.FirstName == "John2");
Console.WriteLine(p1.FirstName);
