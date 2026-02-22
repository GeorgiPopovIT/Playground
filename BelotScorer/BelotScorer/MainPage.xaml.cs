using BelotScorer.Data;
using BelotScorer.Services;

namespace BelotScorer;

public partial class MainPage : ContentPage
{
    private readonly GameRepository _gameRepository;
    private readonly LocalizationService _localizationService;

    public MainPage(GameRepository gameRepository, LocalizationService localizationService)
    {
        InitializeComponent();
        _gameRepository = gameRepository;
        _localizationService = localizationService;

        // Subscribe to language changes to refresh the UI
        _localizationService.PropertyChanged += (s, e) => UpdateUI();
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Force UI refresh by re-binding
        BindingContext = null;
        BindingContext = this;
    }

    async void GoToCreateGame(object sender, EventArgs args)
    {
        var lastGame = await this._gameRepository.GetLastGameAsync();
        if (lastGame is not null && lastGame.IsGameFinished is false)
        {
            bool toContinue = await Shell.Current.DisplayAlertAsync(
                _localizationService["ContinueGameTitle"], 
                _localizationService["ContinueGameMessage"], 
                _localizationService["Yes"], 
                _localizationService["No"]);

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

    void OnLanguageButtonClicked(object sender, EventArgs args)
    {
        _localizationService.SwitchLanguage();
    }
}

