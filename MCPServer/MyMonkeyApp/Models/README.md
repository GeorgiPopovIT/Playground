# Monkey Models Documentation

This document describes the data models used in the MyMonkeyApp console application for managing monkey species data.

## Model Classes

### `Monkey`
The main domain model representing a monkey species with comprehensive information.

**Properties:**
- `Name` (string): The name of the monkey species
- `Location` (string): Geographical location where the species is found
- `Details` (string): Detailed description of the species
- `Image` (string): URL to the monkey's image
- `Population` (int): Estimated population count
- `Latitude` (double): Latitude coordinate of primary habitat
- `Longitude` (double): Longitude coordinate of primary habitat

**Computed Properties:**
- `Coordinates` (string): Formatted coordinate string
- `IsEndangered` (bool): True if population < 5000
- `ConservationStatus` (ConservationStatus): Status based on population
- `FormattedPopulation` (string): Human-readable population format (e.g., "1.5K", "2.1M")

**Key Methods:**
- `ToString()`: Basic monkey information
- `ToDetailedString()`: Comprehensive formatted display with emojis
- `Equals()` / `GetHashCode()`: Equality comparison based on name

### `MonkeyCollection`
A collection wrapper for managing multiple monkey instances.

**Properties:**
- `Monkeys` (List<Monkey>): The list of monkeys
- `Count` (int): Total number of monkeys
- `IsEmpty` (bool): True if collection is empty
- `EndangeredMonkeys` (IEnumerable<Monkey>): Filtered endangered species
- `TotalPopulation` (long): Sum of all monkey populations

**Key Methods:**
- `Add(Monkey)`: Add a monkey to the collection
- `FindByName(string)`: Case-insensitive name search
- `GetByLocation(string)`: Filter by location (partial match)
- `GetRandomMonkey()`: Returns a random monkey from collection

### `MonkeyDto`
Data Transfer Object for JSON serialization/deserialization with MCP server.

**Features:**
- JSON property name mapping with `[JsonPropertyName]` attributes
- `ToMonkey()`: Converts DTO to domain model
- `FromMonkey(Monkey)`: Static method to create DTO from domain model

### `ConservationStatus` (Enum)
Represents conservation status based on population:
- `CriticallyEndangered`: < 1,000
- `Endangered`: 1,000 - 4,999
- `Vulnerable`: 5,000 - 9,999
- `NearThreatened`: 10,000 - 19,999
- `LeastConcern`: 20,000+

**Extension Methods:**
- `GetConservationStatus(int)`: Get status from population
- `GetDescription(ConservationStatus)`: Human-readable description with emoji
- `GetEmoji(ConservationStatus)`: Status emoji icon

## Usage Examples

### Creating a Monkey
```csharp
var monkey = new Monkey(
    "Baboon", 
    "Africa & Asia", 
    "Baboons are African and Arabian Old World monkeys...", 
    "https://example.com/baboon.jpg",
    10000,
    -8.783195,
    34.508523);
```

### Working with Collections
```csharp
var collection = new MonkeyCollection();
collection.Add(monkey);

var randomMonkey = collection.GetRandomMonkey();
var africaMonkeys = collection.GetByLocation("Africa");
var endangered = collection.EndangeredMonkeys;
```

### Conservation Status
```csharp
var status = monkey.ConservationStatus; // ConservationStatus.NearThreatened
var description = status.GetDescription(); // "🔵 Near Threatened"
var emoji = status.GetEmoji(); // "🔵"
```

### JSON Serialization
```csharp
// Deserialize from MCP server response
var dto = JsonSerializer.Deserialize<MonkeyDto>(jsonString);
var monkey = dto.ToMonkey();

// Serialize for API calls
var dto = MonkeyDto.FromMonkey(monkey);
var json = JsonSerializer.Serialize(dto);
```

## Design Patterns Used

1. **Data Transfer Object (DTO)**: `MonkeyDto` separates serialization concerns from domain logic
2. **Value Object**: `Monkey` with proper equality implementation
3. **Collection Wrapper**: `MonkeyCollection` provides typed operations
4. **Extension Methods**: `ConservationStatusExtensions` for status operations
5. **Factory Pattern**: Static creation methods in DTOs

## Thread Safety
- All model classes are **not thread-safe** by design for performance
- `MonkeyCollection` mutations should be synchronized in multi-threaded scenarios
- Computed properties are safe as they don't modify state

## Validation
- Constructor parameters are validated with `ArgumentNullException`
- String properties default to `string.Empty` (never null)
- Population is validated to be non-negative in business logic layer
