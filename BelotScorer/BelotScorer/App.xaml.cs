using BelotScorer.Data;

namespace BelotScorer;

public partial class App : Application
{
	public static GameRepository GameRepo { get; private set; }
	public App(GameRepository gameRepo)
	{
		
		InitializeComponent();

		MainPage = new AppShell();
		
		

		GameRepo = gameRepo;
	}
}
