using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.Models;
public partial class GameViewModel : ObservableObject
{
    public GameViewModel()
    {
        
    }

    [ObservableProperty]
    bool isGameFinished;

    [ObservableProperty]
    string team1Name;

    [ObservableProperty]
    string team2Name;

    [RelayCommand]
    async Task GetGame()
    {

    }
}
