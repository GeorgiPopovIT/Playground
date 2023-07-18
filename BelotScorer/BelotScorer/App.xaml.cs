using BelotScorer.Data;
using BelotScorer.ViewModels;

namespace BelotScorer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
