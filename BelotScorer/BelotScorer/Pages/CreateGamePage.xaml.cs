using BelotScorer.ViewModels;

namespace BelotScorer.Pages;

public partial class CreateGamePage : ContentPage
{
    public CreateGamePage(CreateGameViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    private void Entry_TeamName_Changed(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entry1.Text) && !string.IsNullOrWhiteSpace(entry2.Text))
        {
            btn_Create.IsEnabled = true;

            btn_Create.BackgroundColor = Colors.Blue;
        }
        else
        {
            btn_Create.IsEnabled = false;

            btn_Create.BackgroundColor = Colors.Grey;
        }
    }
}