using BelotScorer.Data;

namespace BelotScorer;

public partial class MainPage : ContentPage
{
    private readonly GameRepository _gameRepository;
    public MainPage(GameRepository gameRepository)
    {
        InitializeComponent();
        this._gameRepository = gameRepository;
    }

    async void GoToCreateGame(object sender, EventArgs args)
    {
        var lastGame = await this._gameRepository.GetLastGameAsync();
        if (lastGame is not null && lastGame.IsGameFinished is false)
        {
            bool toContinue = await Shell.Current.DisplayAlert("Белот", "Искате ли да продължите играта?", "Да", "Не");

            if (toContinue)
            {
                await Shell.Current.GoToAsync("playGame", true,
                    new Dictionary<string, object>
                    {
                        {"Game", lastGame}
                    });

                return;
            }
            else
            {
                await this._gameRepository.DeleteGameAsync(lastGame);
                await this._gameRepository.DeleteAllPoints();
            }
        }
        await Shell.Current.GoToAsync("createGame");
    }
}

