using BelotScorer.Data;
using BelotScorer.Data.Models;
using BelotScorer.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.ViewModels;

[QueryProperty("Team1Name", "Team1Name")]
[QueryProperty("Team2Name", "Team2Name")]
public partial class CreateGameViewModel : ObservableObject
{
    private readonly GameRepository _gameRepository;
    private readonly LocalizationService _localizationService;

    [ObservableProperty]
    Game game;

    [ObservableProperty]
    string team1Name;

    [ObservableProperty]
    string team2Name;

    public CreateGameViewModel(GameRepository gameRepo, LocalizationService localizationService)
    {
        _gameRepository = gameRepo;
        _localizationService = localizationService;

        // Set default team names based on current language
        Team1Name = _localizationService["DefaultTeam1Name"];
        Team2Name = _localizationService["DefaultTeam2Name"];

        // Subscribe to language changes to update default team names
        _localizationService.PropertyChanged += (s, e) =>
        {
            Team1Name = _localizationService["DefaultTeam1Name"];
            Team2Name = _localizationService["DefaultTeam2Name"];
        };
    }

    [RelayCommand]
    async Task CreateGame()
    {
        if (this.Team1Name is null || this.Team2Name is null)
        {
            return;
        }

        var currentGame = await this._gameRepository.CreateGame(this.Team1Name, this.Team2Name);

        await Shell.Current.GoToAsync("playGame", new Dictionary<string, object>
        {
            {"Game", currentGame}
        });

    }
}
