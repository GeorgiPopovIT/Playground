using DE_Bundesliga_Predictor;

FootballResultModel.ModelInput sampleData = new FootballResultModel.ModelInput()
{
    Nation = @"Germany",
    League = @"Bundesliga",
    Date = 26F,
    Month = 1F,
    Year = 2023F,
    Home = @"Augsburg",
    Away = @"Werder Bremen",
    MissingHomeCount = 2F,
    MissingAwayCount = 4F,
    Bet365HC = 3.10F,
    Bet365DC = 3.75F,
    Bet365AC = 2.56F
};

// Make a single prediction on the sample data and print results
var predictionResult = FootballResultModel.Predict(sampleData);

Console.WriteLine($"Predicted HG: {predictionResult.Score}");


