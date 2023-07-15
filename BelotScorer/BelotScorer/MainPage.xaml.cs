using BelotScorer.Data;
using BelotScorer.Models;
using BelotScorer.Pages;

namespace BelotScorer;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Routing.RegisterRoute("createGame", typeof(CreateGamePage));
    }



    async void GoToCreateGame(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("createGame");
    }
}

