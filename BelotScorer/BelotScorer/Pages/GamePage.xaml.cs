using BelotScorer.ViewModels;

namespace BelotScorer.Pages;

public partial class GamePage : ContentPage
{
    GameViewModel _gameViewModel;
    public GamePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = gameViewModel;
        this._gameViewModel = gameViewModel;
    }

    private void Entry_TeamName_Changed(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entry1.Text) && !string.IsNullOrWhiteSpace(entry2.Text))
        {
            btn_Create.IsEnabled = true;

            btn_Create.BackgroundColor = Colors.Blue;
        }
        else
        {
            btn_Create.IsEnabled = false;

            btn_Create.BackgroundColor = Colors.Grey;
        }
    }

    private async  void Save_Points_Clicked(object sender, EventArgs e)
    {
        var isCurrentGameFinished = this._gameViewModel.Game.IsGameFinished;

        if (isCurrentGameFinished)
        {
            bool answer = await DisplayAlert("Белот", "Играта на белот приключи ли?", "Да", "Не");

            if (answer)
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
        }
    }
}