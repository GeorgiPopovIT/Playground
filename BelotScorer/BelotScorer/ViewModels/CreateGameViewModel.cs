﻿using BelotScorer.Data;
using BelotScorer.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BelotScorer.ViewModels;

[QueryProperty("Team1Name","Team1Name")]
[QueryProperty("Team2Name", "Team2Name")]
public partial class CreateGameViewModel : ObservableObject
{
    GameRepository _gameRepository;

    [ObservableProperty]
    Game game;

    [ObservableProperty]
    string team1Name = "Ние";

    [ObservableProperty]
    string team2Name = "Вие";

    public CreateGameViewModel(GameRepository gameRepo)
    {
        this._gameRepository = gameRepo;
    }

    [RelayCommand]
    async void CreateGame()
    {
        if (team1Name is null || team2Name is null)
        {
            return;
        }
        //var currGameId = await this._gameRepository.CreateGame(game.Team1Name, game.Team2Name);

        //  var currentGame = await this._gameRepository.GetGameAsync(currGameId);
        var currentGame = new Game
        {
            Team1Name = team1Name,
            Team2Name = team2Name,
        };
        await Shell.Current.GoToAsync("playGame", new Dictionary<string, object>
        {
            {"CurrentGame", currentGame}
        });
        //await Task.WhenAll(this._gameRepository.CreateGame(team1Name, team2Name),
        //    Shell.Current.GoToAsync("playGame"));

    }
}
