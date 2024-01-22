using England_ML;

var sampleData = new EPL_Model.ModelInput()
{
	Team = @"Aston Villa",
	Rk = 2F,
	MP = 42F,
	W = 21F,
	D = 11F,
	L = 10F,
	GF = 57F,
	GA = 40F,
	GD = 17F,
	Pts = 74F,
	Notes = "",
};

var result = EPL_Model.Predict(sampleData);
Console.WriteLine(result);

