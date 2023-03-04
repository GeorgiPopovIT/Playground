using FootballResultModel_Cдадс;

FootballResultModel.ModelInput sampleData = new FootballResultModel.ModelInput()
{
    Nation = @"Germany",
    League = @"Bundesliga",
    Date = 28F,
    Month = 1F,
    Year = 2023F,
    Home = @"Freiburg",
    Away = @"Augsburg",
    MissingHomeCount = 2F,
    MissingAwayCount = 5F,
    Bet365HC = 1.60F,
    Bet365DC = 4.20F,
    Bet365AC = 5.25F
};


// Make a single prediction on the sample data and print results
var predictionResult = FootballResultModel.Predict(sampleData);

Console.WriteLine($"Predicted HG: {predictionResult.Score}");

