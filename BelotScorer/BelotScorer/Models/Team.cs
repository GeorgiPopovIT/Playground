using SQLite;

namespace BelotScorer.Models;

[Table("teams")]
public class Team
{
    [PrimaryKey, AutoIncrement]

    public int Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; }

    public int TotalPoints { get; set; }

    public ICollection<Result> Results { get; set; } = new HashSet<Result>();

    public int GameId { get; set; }
    public Game Game { get; set; }
}
