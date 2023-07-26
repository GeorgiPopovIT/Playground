using BelotScorer.Data;
using BelotScorer.ViewModels;

namespace BelotScorer.Views;

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
        if (!string.IsNullOrWhiteSpace(entry1.Text) && !string.IsNullOrWhiteSpace(entry2.Text)
            && entry1.Text.All(x => char.IsDigit(x)) && entry2.Text.All(x => char.IsDigit(x)))
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

    private  void Save_Points_Clicked(object sender, EventArgs e)
    {
        this.entry1.Text = null;
        this.entry2.Text = null;
    }
}