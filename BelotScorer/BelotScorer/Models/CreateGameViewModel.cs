using BelotScorer.Data;
using BelotScorer.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.Models;

public partial class CreateGameViewModel : ObservableObject
{
    GameRepository _gameRepo;

    [ObservableProperty]
    string team1Name;

    [ObservableProperty]
    string team2Name;
    public CreateGameViewModel(GameRepository gameRepo)
    {
        this._gameRepo = gameRepo;
        Routing.RegisterRoute("playGame",typeof(GamePage));
    }

    [RelayCommand]
     void CreateGame()
    {
         this._gameRepo.CreateGame(team1Name, team2Name);

         Shell.Current.GoToAsync("createGame");

    }
}
