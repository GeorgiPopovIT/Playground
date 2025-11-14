namespace MyMonkeyApp.Models;

/// <summary>
/// Represents the conservation status of a monkey species based on population size.
/// </summary>
public enum ConservationStatus
{
    /// <summary>
    /// Population is critically low (less than 1,000).
    /// </summary>
    CriticallyEndangered,

    /// <summary>
    /// Population is endangered (1,000 - 4,999).
    /// </summary>
    Endangered,

    /// <summary>
    /// Population is vulnerable (5,000 - 9,999).
    /// </summary>
    Vulnerable,

    /// <summary>
    /// Population is near threatened (10,000 - 19,999).
    /// </summary>
    NearThreatened,

    /// <summary>
    /// Population is of least concern (20,000+).
    /// </summary>
    LeastConcern
}

/// <summary>
/// Provides extension methods for working with conservation status.
/// </summary>
public static class ConservationStatusExtensions
{
    /// <summary>
    /// Gets the conservation status based on population size.
    /// </summary>
    /// <param name="population">The population size.</param>
    /// <returns>The appropriate conservation status.</returns>
    public static ConservationStatus GetConservationStatus(this int population)
    {
        return population switch
        {
            < 1000 => ConservationStatus.CriticallyEndangered,
            < 5000 => ConservationStatus.Endangered,
            < 10000 => ConservationStatus.Vulnerable,
            < 20000 => ConservationStatus.NearThreatened,
            _ => ConservationStatus.LeastConcern
        };
    }

    /// <summary>
    /// Gets a descriptive string for the conservation status.
    /// </summary>
    /// <param name="status">The conservation status.</param>
    /// <returns>A descriptive string for the status.</returns>
    public static string GetDescription(this ConservationStatus status)
    {
        return status switch
        {
            ConservationStatus.CriticallyEndangered => "🔴 Critically Endangered",
            ConservationStatus.Endangered => "🟠 Endangered",
            ConservationStatus.Vulnerable => "🟡 Vulnerable",
            ConservationStatus.NearThreatened => "🔵 Near Threatened",
            ConservationStatus.LeastConcern => "🟢 Least Concern",
            _ => "❓ Unknown"
        };
    }

    /// <summary>
    /// Gets the emoji icon for the conservation status.
    /// </summary>
    /// <param name="status">The conservation status.</param>
    /// <returns>An emoji representing the status.</returns>
    public static string GetEmoji(this ConservationStatus status)
    {
        return status switch
        {
            ConservationStatus.CriticallyEndangered => "🔴",
            ConservationStatus.Endangered => "🟠",
            ConservationStatus.Vulnerable => "🟡",
            ConservationStatus.NearThreatened => "🔵",
            ConservationStatus.LeastConcern => "🟢",
            _ => "❓"
        };
    }
}
