using Positive_Or_Negative_Opinion;
using System.Text;

Console.OutputEncoding = UnicodeEncoding.UTF8;

var sampleData = new Positive_Negative.ModelInput()
{
    Col0 = @"Готин човек си",
};

//Load model and predict output
var result = Positive_Negative.Predict(sampleData);


var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";
Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {sentiment}");
