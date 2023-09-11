using AngleSharp;
using System.ComponentModel;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;


var config = Configuration.Default.WithDefaultLoader();

var context = BrowsingContext.New(config);


BackgroundWorker worker = new BackgroundWorker
{
    WorkerReportsProgress = true,
    WorkerSupportsCancellation = true
};

worker.DoWork += DoWork;
worker.ProgressChanged += ReportProgress;
worker.RunWorkerCompleted += OnWorkCompleted;

worker.RunWorkerAsync();

Console.WriteLine("Press enter in 5 seconds");
Console.ReadLine();

if (worker.IsBusy)
{
    Console.WriteLine("Finished!");
    return;
}

 void DoWork(object? sender, DoWorkEventArgs e)
{
    for (int i = 0; i <= 100; i += 10)
    {
        if (worker.CancellationPending)
        {
            e.Cancel = true;
            return;
        }

        worker.ReportProgress(i + 1);
        Thread.Sleep(1000);
    }

    e.Result = 124;
};
void OnWorkCompleted(object? sender, RunWorkerCompletedEventArgs e)
{
    if (e.Cancelled)
    {
        Console.WriteLine("you canceled");
    }
    else if (e.Error is not null)
    {
        Console.WriteLine("Worker exception: " + e.Error);
    }
    else
    {
        Console.WriteLine("Complete: " + e.Result);
    }
}

void ReportProgress(object? sender, ProgressChangedEventArgs e)
{
    Console.WriteLine("Reached: " + e.ProgressPercentage + "%");
}