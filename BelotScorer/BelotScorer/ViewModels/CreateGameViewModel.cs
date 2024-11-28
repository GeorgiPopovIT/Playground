using BelotScorer.Data;
using BelotScorer.Data.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.ViewModels;

[QueryProperty("Team1Name", "Team1Name")]
[QueryProperty("Team2Name", "Team2Name")]
public partial class CreateGameViewModel : ObservableObject
{
    GameRepository _gameRepository;

    [ObservableProperty]
    Game game;

    [ObservableProperty]
    string team1Name = "Ние";

    [ObservableProperty]
    string team2Name = "Вие";

    public CreateGameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
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
