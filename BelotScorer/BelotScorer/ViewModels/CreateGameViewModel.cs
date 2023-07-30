using BelotScorer.Data;
using BelotScorer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.ViewModels;

[QueryProperty("Team1Name","Team1Name")]
[QueryProperty("Team2Name", "Team2Name")]
public partial class CreateGameViewModel : ObservableObject
{
    GameRepository _gameRepo;

    [ObservableProperty]
    Game game;

    [ObservableProperty]
    string team1Name = "Ние";

    [ObservableProperty]
    string team2Name = "Вие";

    public CreateGameViewModel(GameRepository gameRepo)
    {
        this._gameRepo = gameRepo;
    }

    [RelayCommand]
    async Task CreateGame()
    {
        if (team1Name is null || team2Name is null)
        {
            return;
        }
        //var currGameId = await this._gameRepo.CreateGame(game.Team1Name, game.Team2Name);

        //  var currentGame = await this._gameRepo.GetGameAsync(currGameId);
        var currentGame = new Game
        {
            Team1Name = team1Name,
            Team2Name = team2Name,
        };
        await Shell.Current.GoToAsync("playGame", new Dictionary<string, object>
        {
            {"CurrentGame", currentGame}
        });
        //await Task.WhenAll(this._gameRepo.CreateGame(team1Name, team2Name),
        //    Shell.Current.GoToAsync("playGame"));

    }
}
