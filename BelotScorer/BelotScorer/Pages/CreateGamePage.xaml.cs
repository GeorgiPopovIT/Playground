using BelotScorer.Data;

namespace BelotScorer.Pages;

public partial class CreateGamePage : ContentPage
{
	private readonly GameRepository _gameRepository;

	public CreateGamePage(GameRepository gameRepository)
	{
		InitializeComponent();

        this._gameRepository = gameRepository;
    }

	async void OnTeamsReady(object sender, EventArgs args)
	{

	}
}