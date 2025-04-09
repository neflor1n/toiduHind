using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.HomePage;

public partial class HomePage : ContentPage
{
    private bool isMenuVisible = false;
    private const int menuWidth = 250;
    private readonly User _loggedInUser;

    public HomePage(User user)
    {
        InitializeComponent();
        _loggedInUser = user;

        SetUserGreeting();

        SlidingMenu.TranslationX = -menuWidth;
        Overlay.IsVisible = false;
        Overlay.InputTransparent = true;
    }

    private void SetUserGreeting()
    {
        if (!string.IsNullOrEmpty(_loggedInUser?.Name))
            GreetingLabel.Text = $"Tere, {_loggedInUser.Name}";
        else
            GreetingLabel.Text = "Tere!";
    }

    private async void OnMenuButtonClicked(object sender, EventArgs e)
    {
        if (isMenuVisible)
            await HideMenu();
        else
            await ShowMenu();
    }

    private async Task ShowMenu()
    {
        Overlay.IsVisible = true;
        Overlay.InputTransparent = false;
        await Overlay.FadeTo(0.5, 250);
        await SlidingMenu.TranslateTo(0, 0, 250, Easing.CubicInOut);
        isMenuVisible = true;
    }

    private async Task HideMenu()
    {
        await SlidingMenu.TranslateTo(-menuWidth, 0, 250, Easing.CubicInOut);
        await Overlay.FadeTo(0, 250);
        Overlay.IsVisible = false;
        Overlay.InputTransparent = true;
        isMenuVisible = false;
    }

    private async void OnOptionSelected(object sender, EventArgs e)
    {
        var btn = sender as Button;
        await DisplayAlert("Selected", $"You selected {btn?.Text}", "OK");
        await HideMenu();
    }

    private async void OnOverlayTapped(object sender, EventArgs e)
    {
        await HideMenu();
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await HideMenu();
        await Navigation.PushAsync(new SettingsPage.SettingsPage(_loggedInUser));
    }

}
