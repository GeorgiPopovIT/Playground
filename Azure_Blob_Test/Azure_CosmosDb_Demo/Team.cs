using Newtonsoft.Json;
//For some reason not work with Sytem.Text.Json yet.
using System.Text.Json.Serialization;

namespace Azure_CosmosDb_Demo;
//case-sensitive
//internal record Team(string id, string name, string stadium, DateTimeOffset createdDate, int playersCount);

internal class Team
{
    [JsonProperty("id")]
    public string? Id { get; init; }

    [JsonProperty("name")]
    public string? Name { get; init; }

    [JsonProperty("stadium")]
    public string? Stadium { get; init; }

    [JsonProperty("createdDate")]
    public DateTimeOffset CreatedDate { get; set; }

    [JsonProperty("playersCount")]
    public int PlayersCount { get; init; }
}
