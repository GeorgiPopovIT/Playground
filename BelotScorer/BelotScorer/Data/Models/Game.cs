using SQLite;

namespace BelotScorer.Models;

[Table("games")]
public class Game
{
    [PrimaryKey, AutoIncrement]

    public int Id { get; set; }

    public string Team1Name { get; set; }

    public string Team2Name { get; set; }

    public int Team1FinalPoints { get; set; }

    public int Team2FinalPoints { get; set; }

    public List<short> Team1Points { get; init; } = new();

    public List<short> Team2Points { get; init; } = new();

}