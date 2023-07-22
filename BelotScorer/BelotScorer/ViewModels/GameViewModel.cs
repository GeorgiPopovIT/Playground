using BelotScorer.Data;
using BelotScorer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BelotScorer.ViewModels;

[QueryProperty(nameof(Game), "CurrentGame")]
public partial class GameViewModel : ObservableObject
{
    GameRepository _gameRepository;

    public GameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
        //this.pointsTeam1 = new ObservableCollection<short>();
        //this.pointsTeam2 = new ObservableCollection<short>();
    }

    [ObservableProperty]
    short team1PointsToAdd;

    [ObservableProperty]
    short team2PointsToAdd;

    [ObservableProperty]
    Game game;

    [RelayCommand]
    async Task AddPointsToTeams()
    {
        var isGameFinished = this._gameRepository.SavePointsToTeams(this.game, team1PointsToAdd, team2PointsToAdd);

        if (isGameFinished)
        {
            //bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");

        }
        else
        {

        }
    }
}
