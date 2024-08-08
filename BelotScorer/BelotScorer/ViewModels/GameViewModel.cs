using BelotScorer.Data;
using BelotScorer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BelotScorer.ViewModels;

[QueryProperty("Game", "CurrentGame")]
[QueryProperty("Team1Points","team1Points")]
[QueryProperty("Team2Points","team2Points")]
public partial class GameViewModel : ObservableObject
{
    GameRepository _gameRepository;

    public GameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
    }

    public ObservableCollection<string> Team1Points { get; } = new();

    public ObservableCollection<string> Team2Points { get; } = new();

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
                await this._gameRepository.SavePointsToTeams(this.game, team1PointsToAdd, team2PointsToAdd);

                await this._gameRepository.UpdateGameAsync(this.game);

                if (this.Game.Team1Score == 0 && this.Game.Team2Score == 0)
                {
                    this.Team1Points.Add($"0 - {this.team1PointsToAdd}");
                    this.Team2Points.Add($"0 - {this.team2PointsToAdd}");
                }
                else
                {
                    this.Team1Points.Add($"{this.team1PointsToAdd} - {this.Game.Team1Score}");
                    this.Team2Points.Add($"{this.team2PointsToAdd} - {this.Game.Team2Score}");
                }

                var isCurrentGameFinished = this.game.IsGameFinished;

                if (isCurrentGameFinished)
                {
                    bool answer = await Shell.Current.DisplayAlert("Белот", "Играта на белот приключи ли?", "Да", "Не");

                    if (answer)
                    {
                        await this._gameRepository.DeleteGameAsync(this.game);
                        await this._gameRepository.DeleteAllPoints();

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
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }
}
