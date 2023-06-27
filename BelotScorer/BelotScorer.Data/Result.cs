namespace BelotScorer.Data;

public class Result
{
    public int Id { get; set; }

    public int Points { get; set; }

    public int GameId { get; set; }
    public Game Game { get; set; }
}
