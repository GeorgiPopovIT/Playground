using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel;
using static BelotScorer.Common.Constants;

namespace BelotScorer.Models;

[Table("games")]
public partial class Game : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(TEAM_NAME_MAX_LENGTH)]
    public  string Team1Name { get; set; }

    [MaxLength(TEAM_NAME_MAX_LENGTH)]
    public  string Team2Name { get; set; }

    public int Team1FinalPoints { get; set; }

    public int Team2FinalPoints { get; set; }

    public bool IsGameFinished { get; set; }

    public int RoundNumber { get; set; }

    [ObservableProperty]
    int team1Score;

    [ObservableProperty]
    int team2Score;
}