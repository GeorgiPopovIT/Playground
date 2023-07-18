namespace BelotScorer;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void GoToCreateGame(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("createGame");
    }
}

