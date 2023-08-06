using BelotScorer.Data;
using BelotScorer.Models;

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
        Assert.AreEqual(this._game.Team1FinalPoints, team1Points);
        Assert.AreEqual(this._game.Team2FinalPoints,team2Points);

        Assert.AreEqual(this._game.Team1Points.Count, 1);
        Assert.AreEqual(this._game.Team2Points.Count, 1);
    }
}