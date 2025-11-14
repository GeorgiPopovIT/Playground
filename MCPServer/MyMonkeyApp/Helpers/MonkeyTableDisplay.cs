using MyMonkeyApp.Models;
using System.Text;

namespace MyMonkeyApp.Helpers;

/// <summary>
/// Provides methods for displaying monkey data in formatted tables.
/// </summary>
public static class MonkeyTableDisplay
{
    /// <summary>
    /// Displays a collection of monkeys in a formatted table.
    /// </summary>
    /// <param name="monkeys">The collection of monkeys to display.</param>
    public static void DisplayMonkeysTable(IEnumerable<Monkey> monkeys)
    {
        if (!monkeys.Any())
        {
            Console.WriteLine("🐒 No monkeys found!");
            return;
        }

        var monkeyList = monkeys.ToList();
        
        // Display header
        Console.WriteLine();
        Console.WriteLine("🐵 MONKEY SPECIES DATABASE 🐵");
        Console.WriteLine(new string('=', 120));
        
        // Display table header
        DisplayTableHeader();
        Console.WriteLine(new string('-', 120));

        // Display each monkey
        foreach (var monkey in monkeyList)
        {
            DisplayMonkeyRow(monkey);
        }

        Console.WriteLine(new string('=', 120));
        DisplaySummary(monkeyList);
    }

    /// <summary>
    /// Displays the table header with column names.
    /// </summary>
    private static void DisplayTableHeader()
    {
        Console.WriteLine($"{"Name",-20} {"Location",-25} {"Population",-12} {"Status",-18} {"Coordinates",-20} {"Endangered",-10}");
    }

    /// <summary>
    /// Displays a single monkey as a table row.
    /// </summary>
    /// <param name="monkey">The monkey to display.</param>
    private static void DisplayMonkeyRow(Monkey monkey)
    {
        var name = TruncateString(monkey.Name, 19);
        var location = TruncateString(monkey.Location, 24);
        var population = monkey.FormattedPopulation.PadLeft(11);
        var status = $"{monkey.ConservationStatus.GetEmoji()} {GetShortStatus(monkey.ConservationStatus)}";
        var coordinates = TruncateString(monkey.Coordinates, 19);
        var endangered = monkey.IsEndangered ? "🚨 YES" : "✅ NO";

        Console.WriteLine($"{name,-20} {location,-25} {population,-12} {status,-18} {coordinates,-20} {endangered,-10}");
    }

    /// <summary>
    /// Displays summary statistics for the monkey collection.
    /// </summary>
    /// <param name="monkeys">The list of monkeys.</param>
    private static void DisplaySummary(List<Monkey> monkeys)
    {
        var totalSpecies = monkeys.Count;
        var totalPopulation = monkeys.Sum(m => (long)m.Population);
        var endangeredCount = monkeys.Count(m => m.IsEndangered);
        var criticallyEndangeredCount = monkeys.Count(m => m.ConservationStatus == ConservationStatus.CriticallyEndangered);

        Console.WriteLine();
        Console.WriteLine("📊 SUMMARY STATISTICS:");
        Console.WriteLine($"   🐒 Total Species: {totalSpecies}");
        Console.WriteLine($"   👥 Total Population: {FormatLargeNumber(totalPopulation)}");
        Console.WriteLine($"   🚨 Endangered Species: {endangeredCount} ({(double)endangeredCount / totalSpecies * 100:F1}%)");
        Console.WriteLine($"   🔴 Critically Endangered: {criticallyEndangeredCount}");
        Console.WriteLine();
    }

    /// <summary>
    /// Displays a detailed view of a specific monkey.
    /// </summary>
    /// <param name="monkey">The monkey to display in detail.</param>
    public static void DisplayMonkeyDetails(Monkey monkey)
    {
        Console.WriteLine();
        Console.WriteLine("🔍 MONKEY DETAILS");
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(monkey.ToDetailedString());
        Console.WriteLine(new string('=', 60));
    }

    /// <summary>
    /// Displays monkeys grouped by conservation status.
    /// </summary>
    /// <param name="monkeys">The collection of monkeys to display.</param>
    public static void DisplayMonkeysByConservationStatus(IEnumerable<Monkey> monkeys)
    {
        var monkeyList = monkeys.ToList();
        var groupedMonkeys = monkeyList.GroupBy(m => m.ConservationStatus)
                                      .OrderBy(g => (int)g.Key);

        Console.WriteLine();
        Console.WriteLine("🛡️ MONKEYS BY CONSERVATION STATUS");
        Console.WriteLine(new string('=', 80));

        foreach (var group in groupedMonkeys)
        {
            Console.WriteLine();
            Console.WriteLine($"{group.Key.GetDescription()} ({group.Count()} species):");
            Console.WriteLine(new string('-', 50));
            
            foreach (var monkey in group.OrderBy(m => m.Name))
            {
                Console.WriteLine($"  🐒 {monkey.Name} - {monkey.Location} ({monkey.FormattedPopulation})");
            }
        }
        
        Console.WriteLine(new string('=', 80));
    }

    /// <summary>
    /// Creates an ASCII art table for console display.
    /// </summary>
    /// <param name="monkeys">The collection of monkeys to display.</param>
    public static void DisplayAsciiTable(IEnumerable<Monkey> monkeys)
    {
        var monkeyList = monkeys.Take(10).ToList(); // Limit to first 10 for ASCII art
        
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                           🐵 MONKEY SPECIES TABLE 🐵                                            ║");
        Console.WriteLine("╠══════════════════╦═══════════════════════════╦═════════════╦══════════════════╦════════════════════╦═══════════╣");
        Console.WriteLine("║      Name        ║         Location          ║ Population  ║      Status      ║    Coordinates     ║ Endangered║");
        Console.WriteLine("╠══════════════════╬═══════════════════════════╬═════════════╬══════════════════╬════════════════════╬═══════════╣");

        foreach (var monkey in monkeyList)
        {
            var name = TruncateString(monkey.Name, 16);
            var location = TruncateString(monkey.Location, 25);
            var population = monkey.FormattedPopulation.PadLeft(11);
            var status = $"{monkey.ConservationStatus.GetEmoji()} {GetShortStatus(monkey.ConservationStatus)}";
            var coordinates = TruncateString(monkey.Coordinates, 18);
            var endangered = monkey.IsEndangered ? "🚨 YES" : "✅ NO";

            Console.WriteLine($"║ {name,-16} ║ {location,-25} ║ {population,-11} ║ {status,-16} ║ {coordinates,-18} ║ {endangered,-9} ║");
        }

        Console.WriteLine("╚══════════════════╩═══════════════════════════╩═════════════╩══════════════════╩════════════════════╩═══════════╝");
    }

    /// <summary>
    /// Truncates a string to the specified length and adds ellipsis if needed.
    /// </summary>
    /// <param name="text">The text to truncate.</param>
    /// <param name="maxLength">The maximum length.</param>
    /// <returns>The truncated string.</returns>
    private static string TruncateString(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;

        return text[..(maxLength - 3)] + "...";
    }

    /// <summary>
    /// Gets a short version of the conservation status description.
    /// </summary>
    /// <param name="status">The conservation status.</param>
    /// <returns>A short status description.</returns>
    private static string GetShortStatus(ConservationStatus status)
    {
        return status switch
        {
            ConservationStatus.CriticallyEndangered => "Critical",
            ConservationStatus.Endangered => "Endangered",
            ConservationStatus.Vulnerable => "Vulnerable",
            ConservationStatus.NearThreatened => "Near Threat",
            ConservationStatus.LeastConcern => "Least Concern",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Formats large numbers with appropriate suffixes.
    /// </summary>
    /// <param name="number">The number to format.</param>
    /// <returns>A formatted string representation of the number.</returns>
    private static string FormatLargeNumber(long number)
    {
        return number switch
        {
            >= 1000000 => $"{number / 1000000.0:F1}M",
            >= 1000 => $"{number / 1000.0:F1}K",
            _ => number.ToString()
        };
    }
}
