using BelotScorer.Data;
using BelotScorer.Data.Models;
using BelotScorer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BelotScorer.ViewModels;

[QueryProperty("Game", "Game")]
public partial class GameViewModel : ObservableObject
{
    private readonly GameRepository _gameRepository;
    private readonly LocalizationService _localizationService;

    public GameViewModel(GameRepository gameRepo, LocalizationService localizationService)
    {
        _gameRepository = gameRepo;
        _localizationService = localizationService;
        this.Team1Points = [];
        this.Team2Points = [];

        _ = InitializeGameResult();
    }

    [ObservableProperty]
    Game game;

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
                await this._gameRepository.SavePointToTeam(this.Game.Id, this.Game.Team1Score, this.Game.Team1Name, this.Team1PointsToAdd);
                await this._gameRepository.SavePointToTeam(this.Game.Id, this.Game.Team2Score, this.Game.Team2Name, this.Team2PointsToAdd);

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
                    bool answer = await Shell.Current.DisplayAlert(
                        _localizationService["ContinueGameTitle"], 
                        _localizationService["EndGame"], 
                        _localizationService["Yes"], 
                        _localizationService["No"]);

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

    private async Task InitializeGameResult()
    {
        var lastGame = await this._gameRepository.GetLastGameAsync();
        if (lastGame.IsGameFinished is false)
        {
            var team1Points = await this._gameRepository.GetPointsForTeam(lastGame.Team1Name, lastGame.Id);
            var team2Points = await this._gameRepository.GetPointsForTeam(lastGame.Team2Name, lastGame.Id);

            foreach (var item in team1Points.Select(p => p.Value))
            {
                this.Team1Points.Add(item);
            }
            foreach (var item in team2Points.Select(p => p.Value))
            {
                this.Team2Points.Add(item);
            }
        }
    }

}
