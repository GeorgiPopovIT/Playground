using BelotScorer.ViewModels;
using Microsoft.Maui.Graphics;

namespace BelotScorer.Views;

public partial class GamePage : ContentPage
{
    // Define the button colors as constants for better maintenance
    private static readonly Color ButtonEnabledColor = Color.FromArgb("#8B4513"); // Brown color for enabled
    private static readonly Color ButtonDisabledColor = Color.FromArgb("#A9A9A9"); // Gray color for disabled

    public GamePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = gameViewModel;
    }

    private void Entry_TeamName_Changed(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entry1?.Text) && !string.IsNullOrWhiteSpace(entry2?.Text))
        {
            btn_Create.IsEnabled = true;
            btn_Create.BackgroundColor = ButtonEnabledColor;
        }
        else
        {
            btn_Create.IsEnabled = false;
            btn_Create.BackgroundColor = ButtonDisabledColor;
        }
    }

    private void Save_Points_Clicked(object sender, EventArgs e)
    {
        this.entry1.Text = string.Empty;
        this.entry2.Text = string.Empty;
    }
}