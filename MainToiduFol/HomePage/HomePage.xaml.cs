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
        var button = sender as Button;
        if (button == null) return;

        string categoryName = button.Text.Trim();
        await LoadProductsByCategoryAsync(categoryName);
    }

    private async Task LoadProductsByCategoryAsync(string categoryName)
    {

        // Получаем категорию по имени
        var category = await App.Database.GetCategoryByNameAsync(categoryName);
        if (category == null)
        {
            Console.WriteLine($"Категория '{categoryName}' не найдена в базе данных.");
            await DisplayAlert("Ошибка", "Категория не найдена.", "OK");
            return;
        }

        // Получаем товары и магазины для категории
        var products = await App.Database.GetProductsByCategoryAsync(category.Id);
        var prices = await App.Database.GetAllPricesAsync();
        var stores = await App.Database.GetAllStoresAsync();

        // Объединяем данные
        var productList = products.Select(product =>
        {
            var price = prices.FirstOrDefault(p => p.ProductId == product.Id);
            var store = stores.FirstOrDefault(s => s.Id == price?.StoreId);

            return new
            {
                ProductName = product.Name,
                Brand = product.Brand,
                Price = price?.CurrentPrice ?? 0,
                StoreName = store?.Name ?? "Неизвестный магазин"
            };
        }).ToList();

        // Обновляем интерфейс
        ProductsCollection.ItemsSource = productList;
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
