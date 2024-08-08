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
                var team1Points = await this._gameRepository.GetPointsForTeam(lastGame.Team1Name, lastGame.Id);
                var team2Points = await this._gameRepository.GetPointsForTeam(lastGame.Team2Name, lastGame.Id);

                await Shell.Current.GoToAsync("playGame", new Dictionary<string, object>
                {
                    {"CurrentGame", lastGame},
                    {"Team1Points",team1Points.Select(p => p.Value) },
                    {"Team2Points",team2Points.Select(p => p.Value) }
                });
            }
        }
        await Shell.Current.GoToAsync("createGame");
    }
}

