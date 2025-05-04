using toiduHind.DatabaseModels;

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

        // ���������� �������� � �� ������
        var productList = products.Select(product =>
        {
            var price = prices.FirstOrDefault(p => p.ProductId == product.Id);
            return new
            {
                product.Id,
                product.Name,
                product.Brand,
                CurrentPrice = price?.CurrentPrice ?? 0,
                product.ImageUrl
            };
        }).ToList();

        ProductsCollection.ItemsSource = productList;
    }

    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int productId)
        {
            bool confirm = await DisplayAlert("��������", "�� �������, ��� ������ ������� ���� �������?", "��", "���");
            if (confirm)
            {
                try
                {
                    await App.Database.DeleteProductByIdAsync(productId);
                    await LoadProductsAsync();
                    await DisplayAlert("�����", "������� ������.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("������", $"�� ������� ������� �������: {ex.Message}", "OK");
                }
            }
        }
    }
}
