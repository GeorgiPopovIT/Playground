namespace BelotScorer.Data;

public class Team
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int TotalPoints { get; set; }

    public ICollection<Result> Results { get; set; } = new HashSet<Result>();

    public int GameId { get; set; }
    public Game Game { get; set; }
}
