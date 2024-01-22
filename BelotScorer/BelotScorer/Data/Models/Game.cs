﻿using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
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

    public int Team1FinalPoints => this.Team1Points.Sum();

    public int Team2FinalPoints => this.Team2Points.Sum();

    public bool IsGameFinished { get; set; }

    public int RoundNumber { get; set; }

    [ObservableProperty]
    int team1Score;

    [ObservableProperty]
    int team2Score;

    public List<int> Team1Points { get; init; } = new();

    public List<int> Team2Points { get; init; } = new();

}