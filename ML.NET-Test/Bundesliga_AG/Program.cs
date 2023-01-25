using Bundesliga_AG;

FootballResultModel.ModelInput sampleData = new FootballResultModel.ModelInput()
{
    Nation = @"Germany",
    League = @"Bundesliga",
    Date = 26F,
    Month = 1F,
    Year = 2023F,
    Home = @"Mainz",
    Away = @"Dortmund",
    MissingHomeCount = 5F,
    MissingAwayCount = 4F,
    Bet365HC = 3.10F,
    Bet365DC = 3.75F,
    Bet365AC = 2.15F    
};

// Make a single prediction on the sample data and print results
var predictionResult = FootballResultModel.Predict(sampleData);

Console.WriteLine($"Predicted AG: {predictionResult.Score}");


