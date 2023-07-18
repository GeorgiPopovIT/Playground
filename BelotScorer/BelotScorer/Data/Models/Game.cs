using SQLite;
using static BelotScorer.Common.Constants;

namespace BelotScorer.Models;

[Table("games")]
public class Game
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(TEAM_NAME_MAX_LENGTH)]
    public string Team1Name { get; set; }

    [MaxLength(TEAM_NAME_MAX_LENGTH)]
    public string Team2Name { get; set; }

    public int Team1FinalPoints { get; set; }

    public int Team2FinalPoints { get; set; }

    public bool IsGameFinished { get; set; }

    public List<short> Team1Points { get; init; } = new();

    public List<short> Team2Points { get; init; } = new();

}