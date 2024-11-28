using SQLite;

namespace BelotScorer.Data.Models;

[Table("points")]
public class Point
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int GameId { get; set; }
    public string TeamName { get; set; }

    public string Value { get; set; }
}
