using BelotScorer.Data;
using BelotScorer.Pages;

namespace BelotScorer;

public partial class MainPage : ContentPage
{
    private GameRepository _database;
    public MainPage(GameRepository database)
    {
        InitializeComponent();

        this._database = database;
    }



    async void GoToCreateGame(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new CreateGamePage());
    }
}

