using BelotScorer.Data;
using BelotScorer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BelotScorer.ViewModels;

[QueryProperty("Game", "CurrentGame")]
public partial class GameViewModel : ObservableObject
{
    GameRepository _gameRepository;

    public GameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
    }

    public ObservableCollection<string> PointsTeam1 { get; } = new();

    public ObservableCollection<string> PointsTeam2 { get; } = new();

    [ObservableProperty]
    int team1PointsToAdd;

    [ObservableProperty]
    int team2PointsToAdd;

    [ObservableProperty]
    Game game;

    [RelayCommand]
    async Task AddPointsToTeams()
    {
        if (this.game.IsGameFinished is false)
        {
            try
            {
                //this.pointsTeam1.Add($"{this.game.Team1Score} - {this.team1PointsToAdd}");
                //this.pointsTeam2.Add($"{this.game.Team2Score} - {this.team2PointsToAdd}");

                this._gameRepository.SavePointsToTeams(this.game, team1PointsToAdd, team2PointsToAdd);
                if (this.Game.Team1FinalPoints == this.team1PointsToAdd)
                {
                    this.PointsTeam1.Add($"0 - {this.team1PointsToAdd}");
                    this.PointsTeam2.Add($"0 - {this.team2PointsToAdd}");
                    return;
                }
                this.PointsTeam1.Add($"{this.Game.Team1Score} - {this.team1PointsToAdd}");
                this.PointsTeam2.Add($"{this.Game.Team2Score} - {this.team2PointsToAdd}");

                var isCurrentGameFinished = this.game.IsGameFinished;

                if (isCurrentGameFinished)
                {
                    bool answer = await Shell.Current.DisplayAlert("Белот", "Играта на белот приключи ли?", "Да", "Не");

                    if (answer)
                    {
                        await Shell.Current.GoToAsync("createGame", new Dictionary<string, object>
                    {
                        {"Team1Name", this.game.Team1Name},
                        {"Team2Name", this.game.Team2Name}
                    });
                    }
                    else
                    {
                        this.game.IsGameFinished = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
