using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels.AddDataFiles
{
    public partial class AddProductPage : ContentPage
    {
        private List<Category> _categories;
        private List<Store> _stores;

        public AddProductPage()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Load categories
                _categories = await App.Database.GetAllCategoriesAsync();
                CategoryPicker.ItemsSource = _categories.Select(c => c.Name).ToList();

                // Load stores
                _stores = await App.Database.GetAllStoresAsync();
                StorePicker.ItemsSource = _stores.Select(s => s.Name).ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Andmete laadimise viga: {ex.Message}", "OK");
                await Navigation.PopAsync();
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisesta toote nimi", "OK");
                    return;
                }

                if (CategoryPicker.SelectedIndex < 0)
                {
                    await DisplayAlert("Viga", "Palun vali kategooria", "OK");
                    return;
                }

                if (StorePicker.SelectedIndex < 0)
                {
                    await DisplayAlert("Viga", "Palun vali pood", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(PriceEntry.Text) || !decimal.TryParse(PriceEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal priceValue))
                {
                    await DisplayAlert("Viga", "Palun sisesta korrektne hind", "OK");
                    return;
                }

                decimal? discountPrice = null;
                if (!string.IsNullOrWhiteSpace(DiscountPriceEntry.Text) &&
                    decimal.TryParse(DiscountPriceEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal discount))
                {
                    discountPrice = discount;
                }

                // Create product
                var product = new Product
                {
                    Name = NameEntry.Text,
                    Brand = BrandEntry.Text,
                    CategoryId = _categories[CategoryPicker.SelectedIndex].Id,
                    Description = DescriptionEditor.Text,
                    ImageUrl = ImageUrlEntry.Text,
                    CreatedAt = DateTime.UtcNow
                };

                // Save product to database
                await App.Database.AddProductAsync(product);

                // Create price
                var price = new Price
                {
                    ProductId = product.Id,
                    StoreId = _stores[StorePicker.SelectedIndex].Id,
                    CurrentPrice = priceValue,
                    DiscountPrice = discountPrice,
                    UpdatedAt = DateTime.UtcNow
                };

                // Save price to database
                await App.Database.InsertPriceAsync(price);

                await DisplayAlert("Õnnestus", "Toode on edukalt lisatud", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Toote lisamine ebaõnnestus: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}

