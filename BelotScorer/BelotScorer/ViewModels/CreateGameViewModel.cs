using BelotScorer.Data;
using BelotScorer.Models;
using BelotScorer.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.ViewModels;

public partial class CreateGameViewModel : ObservableObject
{
    GameRepository _gameRepo;

    [ObservableProperty]
    Game game;

    public CreateGameViewModel(GameRepository gameRepo)
    {
        this._gameRepo = gameRepo;
    }

    [RelayCommand]
    async Task CreateGame()
    {
        await this._gameRepo.CreateGame(game.Team1Name, game.Team2Name);

        await Shell.Current.GoToAsync("playGame");

        //await Task.WhenAll(this._gameRepo.CreateGame(team1Name, team2Name),
        //    Shell.Current.GoToAsync("playGame"));

    }
}
