namespace toiduHind.MainToiduFol.HomePage;

public partial class HomePage : ContentPage
{
    private bool isMenuVisible = false; // Флаг состояния меню
    private const int menuWidth = 250;  // Ширина меню

    public HomePage()
    {
        InitializeComponent();
        SlidingMenu.TranslationX = -menuWidth; // Убеждаемся, что меню скрыто при старте
        Overlay.IsVisible = false;
        Overlay.InputTransparent = true;
    }

    private async void OnMenuButtonClicked(object sender, EventArgs e)
    {
        if (isMenuVisible)
        {
            await HideMenu();
        }
        else
        {
            await ShowMenu();
        }
    }

    private async Task ShowMenu()
    {
        Overlay.IsVisible = true;
        Overlay.InputTransparent = false;
        await Overlay.FadeTo(0.5, 250); // Затемняем фон
        await SlidingMenu.TranslateTo(0, 0, 250, Easing.CubicInOut);
        isMenuVisible = true;
    }

    private async Task HideMenu()
    {
        await SlidingMenu.TranslateTo(-menuWidth, 0, 250, Easing.CubicInOut);
        await Overlay.FadeTo(0, 250); // Убираем затемнение
        Overlay.IsVisible = false;
        isMenuVisible = false;
    }

    private async void OnOptionSelected(object sender, EventArgs e)
    {
        await DisplayAlert("Выбрано", $"Ты выбрал {(sender as Button).Text}", "OK");
        await HideMenu(); // Закрываем меню после выбора
    }

    private async void OnOverlayTapped(object sender, EventArgs e)
    {
        await HideMenu(); // Закрываем меню при клике на затемненный фон
    }
}
