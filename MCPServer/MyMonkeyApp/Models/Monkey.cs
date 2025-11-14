namespace MyMonkeyApp.Models;

/// <summary>
/// Represents a monkey species with detailed information including location, population, and coordinates.
/// </summary>
public class Monkey
{
    /// <summary>
    /// Gets or sets the name of the monkey species.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographical location where the monkey species is found.
    /// </summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets detailed information about the monkey species.
    /// </summary>
    public string Details { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the monkey's image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated population of the monkey species.
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the monkey's primary habitat.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the monkey's primary habitat.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets a formatted string representation of the monkey's coordinates.
    /// </summary>
    public string Coordinates => $"{Latitude:F6}, {Longitude:F6}";

    /// <summary>
    /// Gets a value indicating whether this monkey species is endangered (population less than 5000).
    /// </summary>
    public bool IsEndangered => Population < 5000;

    /// <summary>
    /// Gets the conservation status of this monkey species based on its population.
    /// </summary>
    public ConservationStatus ConservationStatus => Population.GetConservationStatus();

    /// <summary>
    /// Gets a formatted population string with appropriate suffix.
    /// </summary>
    public string FormattedPopulation => Population switch
    {
        >= 1000000 => $"{Population / 1000000.0:F1}M",
        >= 1000 => $"{Population / 1000.0:F1}K",
        _ => Population.ToString()
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="Monkey"/> class.
    /// </summary>
    public Monkey()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Monkey"/> class with specified values.
    /// </summary>
    /// <param name="name">The name of the monkey species.</param>
    /// <param name="location">The geographical location of the monkey.</param>
    /// <param name="details">Detailed information about the monkey.</param>
    /// <param name="image">The URL of the monkey's image.</param>
    /// <param name="population">The estimated population.</param>
    /// <param name="latitude">The latitude coordinate.</param>
    /// <param name="longitude">The longitude coordinate.</param>
    public Monkey(string name, string location, string details, string image, int population, double latitude, double longitude)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Location = location ?? throw new ArgumentNullException(nameof(location));
        Details = details ?? throw new ArgumentNullException(nameof(details));
        Image = image ?? throw new ArgumentNullException(nameof(image));
        Population = population;
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Returns a string representation of the monkey with basic information.
    /// </summary>
    /// <returns>A formatted string containing the monkey's name, location, and population.</returns>
    public override string ToString()
    {
        return $"{Name} - {Location} (Population: {FormattedPopulation})";
    }

    /// <summary>
    /// Returns a detailed string representation of the monkey with all available information.
    /// </summary>
    /// <returns>A formatted string containing comprehensive monkey information.</returns>
    public string ToDetailedString()
    {
        var statusInfo = ConservationStatus.GetDescription();
        return $"""
            🐒 {Name} {ConservationStatus.GetEmoji()}
            📍 Location: {Location}
            🌍 Coordinates: {Coordinates}
            👥 Population: {FormattedPopulation}
            🛡️  Status: {statusInfo}
            ℹ️  Details: {Details}
            🖼️  Image: {Image}
            """;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current monkey.
    /// </summary>
    /// <param name="obj">The object to compare with the current monkey.</param>
    /// <returns>true if the specified object is equal to the current monkey; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Monkey other && Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns the hash code for this monkey.
    /// </summary>
    /// <returns>A hash code for the current monkey.</returns>
    public override int GetHashCode()
    {
        return Name.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }
}
