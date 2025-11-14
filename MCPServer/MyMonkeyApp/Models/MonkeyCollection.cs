namespace MyMonkeyApp.Models;

/// <summary>
/// Represents a collection of monkeys returned from the MCP server.
/// </summary>
public class MonkeyCollection
{
    /// <summary>
    /// Gets or sets the list of monkeys.
    /// </summary>
    public List<Monkey> Monkeys { get; set; } = new();

    /// <summary>
    /// Gets the total count of monkeys in the collection.
    /// </summary>
    public int Count => Monkeys.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is empty.
    /// </summary>
    public bool IsEmpty => Monkeys.Count == 0;

    /// <summary>
    /// Gets all endangered monkeys (population less than 5000).
    /// </summary>
    public IEnumerable<Monkey> EndangeredMonkeys => Monkeys.Where(m => m.IsEndangered);

    /// <summary>
    /// Gets the total population of all monkeys in the collection.
    /// </summary>
    public long TotalPopulation => Monkeys.Sum(m => (long)m.Population);

    /// <summary>
    /// Initializes a new instance of the <see cref="MonkeyCollection"/> class.
    /// </summary>
    public MonkeyCollection()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MonkeyCollection"/> class with the specified monkeys.
    /// </summary>
    /// <param name="monkeys">The collection of monkeys to initialize with.</param>
    public MonkeyCollection(IEnumerable<Monkey> monkeys)
    {
        Monkeys = monkeys?.ToList() ?? throw new ArgumentNullException(nameof(monkeys));
    }

    /// <summary>
    /// Adds a monkey to the collection.
    /// </summary>
    /// <param name="monkey">The monkey to add.</param>
    public void Add(Monkey monkey)
    {
        ArgumentNullException.ThrowIfNull(monkey);
        Monkeys.Add(monkey);
    }

    /// <summary>
    /// Finds a monkey by name (case-insensitive).
    /// </summary>
    /// <param name="name">The name of the monkey to find.</param>
    /// <returns>The monkey if found; otherwise, null.</returns>
    public Monkey? FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return Monkeys.FirstOrDefault(m => 
            m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets monkeys filtered by location (case-insensitive partial match).
    /// </summary>
    /// <param name="location">The location to filter by.</param>
    /// <returns>An enumerable of monkeys matching the location filter.</returns>
    public IEnumerable<Monkey> GetByLocation(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            return Enumerable.Empty<Monkey>();

        return Monkeys.Where(m => 
            m.Location.Contains(location, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets a random monkey from the collection.
    /// </summary>
    /// <returns>A random monkey if the collection is not empty; otherwise, null.</returns>
    public Monkey? GetRandomMonkey()
    {
        if (IsEmpty)
            return null;

        var random = new Random();
        int index = random.Next(Monkeys.Count);
        return Monkeys[index];
    }

    /// <summary>
    /// Returns a string representation of the monkey collection.
    /// </summary>
    /// <returns>A formatted string containing collection statistics.</returns>
    public override string ToString()
    {
        var endangeredCount = EndangeredMonkeys.Count();
        return $"Monkey Collection: {Count} species (🚨 {endangeredCount} endangered)";
    }
}
