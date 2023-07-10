using BelotScorer.Data;
using BelotScorer.Pages;

namespace BelotScorer;

public partial class MainPage : ContentPage
{
    //int count = 0;
    private GameRepository _database;
    public MainPage(GameRepository database)
    {
        InitializeComponent();

        this._database = database;
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Clicked {count} time";
    //	else
    //		CounterBtn.Text = $"Clicked {count} times";

    //	SemanticScreenReader.Announce(CounterBtn.Text);
    //}

    async void GoToCreateGame(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new CreateGamePage(_database));
    }
}

