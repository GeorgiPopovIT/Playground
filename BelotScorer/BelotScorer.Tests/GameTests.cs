using BelotScorer.Data;
using BelotScorer.Models;
using NUnit.Framework.Legacy;

namespace BelotScorer.Tests;

public class GameTests
{
    private GameRepository _gameRepository;
    private Game _game;

    [SetUp]
    public void Setup()
    {
        this._gameRepository = new GameRepository();

        this._game = new Game()
        {
            Team1Name = "Nie",
            Team2Name = "Vie"
        };
    }

    [Test]
    [TestCase(25,32)]
    [TestCase(0,125)]
    [TestCase(56,0)]
    public void AddPointsAfterRoundWithPositiveValues(int team1Points, int team2Points)
    {
        //Act
        this._gameRepository.SavePointsToTeams(this._game, team1Points, team2Points);

        //Assert
        Assert.That(team1Points, Is.EqualTo(this._game.Team1FinalPoints));
        Assert.That(team2Points, Is.EqualTo(this._game.Team2FinalPoints));
    }
    [Test]
    public void AddPointsAndTeam1Win()
    {
        //Act
        this._gameRepository.SavePointsToTeams(this._game, 26, 0);
        this._gameRepository.SavePointsToTeams(this._game, 2, 14);
        this._gameRepository.SavePointsToTeams(this._game, 52, 0);
        this._gameRepository.SavePointsToTeams(this._game, 0, 16);
        this._gameRepository.SavePointsToTeams(this._game, 36, 0);
        this._gameRepository.SavePointsToTeams(this._game, 35, 0);

        //Assert
        Assert.That(this._game.IsGameFinished, Is.EqualTo(true));
        Assert.That(this._game.Team1FinalPoints >= 151, Is.EqualTo(true));
    }
    [Test]
    public void AddPointsAndTeam2Win()
    {
        //Act
        this._gameRepository.SavePointsToTeams(this._game, 3, 15);
        this._gameRepository.SavePointsToTeams(this._game, 26, 0);
        this._gameRepository.SavePointsToTeams(this._game, 0, 35);
        this._gameRepository.SavePointsToTeams(this._game, 0, 26);
        this._gameRepository.SavePointsToTeams(this._game, 9, 7);
        this._gameRepository.SavePointsToTeams(this._game, 11, 17);
        this._gameRepository.SavePointsToTeams(this._game, 10, 28);
        this._gameRepository.SavePointsToTeams(this._game, 14, 2);
        this._gameRepository.SavePointsToTeams(this._game, 0, 32);

        //Assert
        Assert.That(this._game.IsGameFinished, Is.EqualTo(true));
        Assert.That(this._game.Team2FinalPoints >= 151, Is.EqualTo(true));
    }

    [Test]
    public void AddScoreWithNegativeNumbers()
    {
        //Act
        this._gameRepository.SavePointsToTeams(this._game, 24, 12);
        this._gameRepository.SavePointsToTeams(this._game, -2, -12);
        this._gameRepository.SavePointsToTeams(this._game, -26, -34);


        //Assert
        ClassicAssert.Negative(this._game.Team1FinalPoints);
        ClassicAssert.Negative(this._game.Team2FinalPoints);

    }

    [Test]
    public void CreateNewGameSuccessfully()
    {
        //Arrange
        var game = new Game
        {
            Team1Name = "Ние",
            Team2Name = "Вие"
        };

        //Assert
        Assert.That(game.Team1Name, Is.EqualTo("Ние"));
        Assert.That(game.Team2Name, Is.EqualTo("Вие"));

    }
}