using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels.EditDataFiles
{
    public partial class EditProductPage : ContentPage
    {
        private readonly int _productId;
        private Product _product;
        private Price _price;
        private List<Category> _categories;
        private List<Store> _stores;

        public EditProductPage(int productId)
        {
            InitializeComponent();
            _productId = productId;
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Load product
                _product = await App.Database.GetProductByIdAsync(_productId);
                if (_product == null)
                {
                    await DisplayAlert("Viga", "Toodet ei leitud", "OK");
                    await Navigation.PopAsync();
                    return;
                }

                // Load price
                var prices = await App.Database.GetAllPricesAsync();
                _price = prices.FirstOrDefault(p => p.ProductId == _productId);

                // Load categories
                _categories = await App.Database.GetAllCategoriesAsync();
                CategoryPicker.ItemsSource = _categories.Select(c => c.Name).ToList();

                // Find category index
                var categoryIndex = _categories.FindIndex(c => c.Id == _product.CategoryId);
                if (categoryIndex >= 0)
                {
                    CategoryPicker.SelectedIndex = categoryIndex;
                }

                // Load stores
                _stores = await App.Database.GetAllStoresAsync();
                StorePicker.ItemsSource = _stores.Select(s => s.Name).ToList();

                // Find store index if we have a price
                if (_price != null)
                {
                    var storeIndex = _stores.FindIndex(s => s.Id == _price.StoreId);
                    if (storeIndex >= 0)
                    {
                        StorePicker.SelectedIndex = storeIndex;
                    }
                }

                // Populate product fields
                NameEntry.Text = _product.Name;
                BrandEntry.Text = _product.Brand;
                DescriptionEditor.Text = _product.Description;
                ImageUrlEntry.Text = _product.ImageUrl;

                // Populate price fields if available
                if (_price != null)
                {
                    PriceEntry.Text = _price.CurrentPrice.ToString();
                    DiscountPriceEntry.Text = _price.DiscountPrice?.ToString() ?? string.Empty;
                }
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

                // Update product data
                _product.Name = NameEntry.Text;
                _product.Brand = BrandEntry.Text;
                _product.CategoryId = _categories[CategoryPicker.SelectedIndex].Id;
                _product.Description = DescriptionEditor.Text;
                _product.ImageUrl = ImageUrlEntry.Text;

                // Update or create price
                if (_price == null)
                {
                    _price = new Price
                    {
                        ProductId = _product.Id,
                        StoreId = _stores[StorePicker.SelectedIndex].Id,
                        CurrentPrice = priceValue,
                        DiscountPrice = discountPrice,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await App.Database.InsertPriceAsync(_price); 
                }
                else
                {
                    _price.StoreId = _stores[StorePicker.SelectedIndex].Id;
                    _price.CurrentPrice = priceValue;
                    _price.DiscountPrice = discountPrice;
                    _price.UpdatedAt = DateTime.UtcNow;
                    await App.Database.UpdatePriceAsync(_price);
                }

                // Save to database
                await App.Database.UpdateProductAsync(_product);
                await DisplayAlert("Õnnestus", "Toote andmed on uuendatud", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Toote uuendamine ebaõnnestus: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
