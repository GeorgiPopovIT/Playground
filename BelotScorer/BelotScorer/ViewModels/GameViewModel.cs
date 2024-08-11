using BelotScorer.Data;
using BelotScorer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace BelotScorer.ViewModels;

[QueryProperty("Game", "CurrentGame")]

public partial class GameViewModel : ObservableObject
{
    private readonly GameRepository _gameRepository;

    public GameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
    }

    [ObservableProperty]
    Game game;

    [ObservableProperty]
    string team2PointsAsString;
    public ObservableCollection<string> Team1Points { get; set; }

    public ObservableCollection<string> Team2Points { get; set; }

    [ObservableProperty]
    int team1PointsToAdd;

    [ObservableProperty]
    int team2PointsToAdd;


    [RelayCommand]
    async Task AddPointsToTeams()
    {
        if (!this.IsGameFinished())
        {
            try
            {
                await this._gameRepository.SavePointsToTeams(this.Game, this.Team1PointsToAdd, this.Team2PointsToAdd);
                if (this.Game.Team1Score == 0 && this.Game.Team2Score == 0)
                {
                    this.Team1Points.Add($"0 - {this.Team1PointsToAdd}");
                    this.Team2Points.Add($"0 - {this.Team2PointsToAdd}");

                    await this._gameRepository.AddPointToEndScore(this.Game, Team1PointsToAdd, Team2PointsToAdd);
                }
                else
                {
                    await this._gameRepository.AddPointToEndScore(this.Game, Team1PointsToAdd, Team2PointsToAdd);

                    this.Team1Points.Add($"{this.Team1PointsToAdd} - {this.Game.Team1Score}");
                    this.Team2Points.Add($"{this.Team2PointsToAdd} - {this.Game.Team2Score}");
                }

                if (this.IsGameFinished())
                {
                    bool answer = await Shell.Current.DisplayAlert("Белот", "Играта на белот приключи ли?", "Да", "Не");

                    if (answer)
                    {
                        await this._gameRepository.DeleteGameAsync(this.Game);
                        await this._gameRepository.DeleteAllPoints();

                        await Shell.Current.GoToAsync("createGame", new Dictionary<string, object>
                        {
                            {"Team1Name", this.Game.Team1Name},
                            {"Team2Name", this.Game.Team2Name}
                        });
                    }
                    else
                    {
                        this.Game.IsGameFinished = false;
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
    }

    private bool IsGameFinished()
        => this.Game.IsGameFinished;

}
