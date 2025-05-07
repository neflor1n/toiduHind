using toiduHind.DatabaseModels;
using toiduHind.MainToiduFol.AdminPanels.EditDataFiles;
using toiduHind.MainToiduFol.AdminPanels.AddDataFiles;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class ProductsPage : ContentPage
{
    public ProductsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadProductsAsync();
    }

    private async Task LoadProductsAsync()
    {
        var products = await App.Database.GetAllProductsAsync();
        var prices = await App.Database.GetAllPricesAsync();

        // Соединение продукта и его ценник
        var productList = products.Select(product =>
        {
            var price = prices.FirstOrDefault(p => p.ProductId == product.Id);
            return new
            {
                product.Id,
                product.Name,
                product.Brand,
                product.CategoryId,
                product.ImageUrl,
                product.Description,
                product.PopularityScore,
                PriceId = price?.Id ?? 0,
                CurrentPrice = price?.CurrentPrice ?? 0,
                DiscountPrice = price?.DiscountPrice,
                StoreId = price?.StoreId ?? 0
            };
        }).ToList();

        ProductsCollection.ItemsSource = productList;
    }

    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int productId)
        {
            bool confirm = await DisplayAlert("Удаление", "Вы уверены, что хотите удалить этот продукт?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    await App.Database.DeleteProductByIdAsync(productId);
                    await LoadProductsAsync();
                    await DisplayAlert("Успех", "Продукт удален.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Не удалось удалить продукт: {ex.Message}", "OK");
                }
            }
        }
    }

    private async void OnAddProductClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddProductPage());
    }

    private async void OnEditProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int productId)
        {
            await Navigation.PushAsync(new EditProductPage(productId));
        }
    }

    private async void OnProductTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int productId)
        {
            try
            {
                // Get the product details
                var product = await App.Database.GetProductByIdAsync(productId);
                if (product != null)
                {
                    var price = (await App.Database.GetAllPricesAsync())
                        .FirstOrDefault(p => p.ProductId == product.Id);

                    var category = await App.Database.GetCategoryByIdAsync(product.CategoryId);

                    // Show product details
                    string details = $"ID: {product.Id}\n" +
                                    $"Name: {product.Name}\n" +
                                    $"Brand: {product.Brand}\n" +
                                    $"Category: {category?.Name ?? "Unknown"}\n" +
                                    $"Price: {price?.CurrentPrice:C}\n" +
                                    $"Discount: {(price?.DiscountPrice.HasValue == true ? price.DiscountPrice.Value.ToString("C") : "None")}\n" +
                                    $"Created: {product.CreatedAt:g}";

                    await DisplayAlert($"Product Details: {product.Name}", details, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load product details: {ex.Message}", "OK");
            }
        }
    }
}
