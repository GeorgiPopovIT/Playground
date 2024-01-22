using PL_champion_2023;

var sampleData = new PL_CHAMPION_2023.ModelInput()
{
    Season = @"2023-24",
    Team = @"Arsenal",
    Pld = 20F,
    W = 12F,
    D = 4F,
    L = 4F,
    GF = 37F,
    GA = 17F,
    GD = 20F,
    Pts = 40F,
};

//Load model and predict output
var result = PL_CHAMPION_2023.Predict(sampleData);
Console.WriteLine(result.Score);
Console.WriteLine($"{sampleData.Team} place: {Math.Round(result.Score,0)}");