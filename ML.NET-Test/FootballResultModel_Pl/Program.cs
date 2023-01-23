using FootballResultModel_PL_EN;

FootballResultModel.ModelInput sampleData = new FootballResultModel.ModelInput()
{
    Match_Number = 204F,
    Round_Number = 21F,
    Date = @"23/01/2023 20:00",
    Location = @"Craven Cottage",
    Home_Team = @"Fulham",
    Away_Team = @"Spurs"
};

var predictionResult = FootballResultModel.Predict(sampleData);

Console.WriteLine($"HG for Fulham:{predictionResult.AG}");