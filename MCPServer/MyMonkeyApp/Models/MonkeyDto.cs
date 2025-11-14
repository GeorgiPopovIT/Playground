using System.Text.Json.Serialization;

namespace MyMonkeyApp.Models;

/// <summary>
/// Data Transfer Object for deserializing monkey data from the MCP server JSON response.
/// </summary>
public class MonkeyDto
{
    /// <summary>
    /// Gets or sets the name of the monkey species.
    /// </summary>
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographical location where the monkey species is found.
    /// </summary>
    [JsonPropertyName("Location")]
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets detailed information about the monkey species.
    /// </summary>
    [JsonPropertyName("Details")]
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the monkey's image.
    /// </summary>
    [JsonPropertyName("Image")]
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated population of the monkey species.
    /// </summary>
    [JsonPropertyName("Population")]
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the monkey's primary habitat.
    /// </summary>
    [JsonPropertyName("Latitude")]
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the monkey's primary habitat.
    /// </summary>
    [JsonPropertyName("Longitude")]
    public double Longitude { get; set; }

    /// <summary>
    /// Converts this DTO to a domain model Monkey object.
    /// </summary>
    /// <returns>A new Monkey instance with the data from this DTO.</returns>
    public Monkey ToMonkey()
    {
        return new Monkey(
            name: Name,
            location: Location,
            details: Details,
            image: Image,
            population: Population,
            latitude: Latitude,
            longitude: Longitude);
    }

    /// <summary>
    /// Creates a MonkeyDto from a domain model Monkey object.
    /// </summary>
    /// <param name="monkey">The monkey to convert.</param>
    /// <returns>A new MonkeyDto instance with the data from the monkey.</returns>
    public static MonkeyDto FromMonkey(Monkey monkey)
    {
        ArgumentNullException.ThrowIfNull(monkey);

        return new MonkeyDto
        {
            Name = monkey.Name,
            Location = monkey.Location,
            Details = monkey.Details,
            Image = monkey.Image,
            Population = monkey.Population,
            Latitude = monkey.Latitude,
            Longitude = monkey.Longitude
        };
    }
}
