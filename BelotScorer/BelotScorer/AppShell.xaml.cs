using BelotScorer.Views;

namespace BelotScorer;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("mainPage", typeof(MainPage));
        Routing.RegisterRoute("createGame", typeof(CreateGamePage));
        Routing.RegisterRoute("playGame", typeof(GamePage));
        Routing.RegisterRoute("endGame", typeof(MainPage));
    }
}
