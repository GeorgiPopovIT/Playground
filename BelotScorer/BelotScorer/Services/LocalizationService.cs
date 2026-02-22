using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace BelotScorer.Services;

public class LocalizationService : INotifyPropertyChanged
{
    public static LocalizationService Instance { get; private set; }

    private readonly ResourceManager _resourceManager;

    public event PropertyChangedEventHandler PropertyChanged;

    public LocalizationService()
    {
        _resourceManager = new ResourceManager("BelotScorer.Resources.Strings.AppResources", typeof(LocalizationService).Assembly);

        // Load saved language preference or default to Bulgarian
        var savedLanguage = Preferences.Get("AppLanguage", "bg");
        SetCulture(savedLanguage);

        // Set static instance for XAML bindings
        Instance = this;
    }

    public CultureInfo CurrentCulture
    {
        get => field;
        private set
        {
            if (field != value)
            {
                field = value;
                CultureInfo.CurrentUICulture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
    }

    public string this[string key]
    {
        get
        {
            try
            {
                return _resourceManager.GetString(key, CurrentCulture) ?? key;
            }
            catch
            {
                return key;
            }
        }
    }

    public void SetCulture(string cultureName)
    {
        try
        {
            var culture = new CultureInfo(cultureName);
            CurrentCulture = culture;
            
            // Save the preference
            Preferences.Set("AppLanguage", cultureName);
        }
        catch (CultureNotFoundException)
        {
            // Fallback to Bulgarian if culture not found
            CurrentCulture = new CultureInfo("bg");
        }
    }

    public void SwitchLanguage()
    {
        var newLanguage = CurrentCulture.TwoLetterISOLanguageName == "bg" ? "en" : "bg";
        SetCulture(newLanguage);
    }

    public string GetCurrentLanguageDisplay()
    {
        return CurrentCulture.TwoLetterISOLanguageName == "bg" ? this["Bulgarian"] : this["English"];
    }
}
