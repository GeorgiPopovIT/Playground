using Azure_CosmosDb_Demo;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

// Get variables, keys...
IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

//var account = config["Account"];
//var key = config["Key"];
var databaseName = config["Database"];
var containerName = config["Container"];
var connectionString = config["ConnectionString"];

//Initialize cosmos client, database, container
var cosmosClient = new CosmosClient(connectionString);

var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");


Container container = cosmosClient.GetContainer(databaseName, containerName);

//create object

//Team team = new(Guid.NewGuid().ToString(), "Slavia stadium", "Slavia stadium", DateTimeOffset.UtcNow, 21);

Team team = new()
{
    Id = Guid.NewGuid().ToString(),
    Name = "Slavia",
    Stadium = "Slavia stadium",
    CreatedDate = DateTimeOffset.UtcNow,
    PlayersCount = 21
};
//add object to container in the db

await container.CreateItemAsync<Team>(team, new PartitionKey(team.Id));

//Read the object
var result = await container.ReadItemAsync<Team>(team.Id, new PartitionKey(team.Id));

Console.WriteLine(result.Resource.ToString());