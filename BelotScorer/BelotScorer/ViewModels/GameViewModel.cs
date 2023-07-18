using BelotScorer.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace BelotScorer.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private GameRepository _gameRepository;

    public GameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
        this.points = new ObservableCollection<short>();
    }

    [ObservableProperty]
    int totalTeam1Points;

    [ObservableProperty]
    int totalTeam2Points;

    [ObservableProperty]
    ObservableCollection<short> points;

}
