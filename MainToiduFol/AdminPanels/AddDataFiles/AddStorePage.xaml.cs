using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels.AddDataFiles
{
    public partial class AddStorePage : ContentPage
    {
        private readonly Database _database;

        public AddStorePage()
        {
            InitializeComponent();
            _database = new Database();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Ошибка", "Пожалуйста, введите название магазина", "OK");
                    return;
                }

                // Check if store with this name already exists
                var existingStore = await _database.GetStoreByNameAsync(NameEntry.Text);
                if (existingStore != null)
                {
                    await DisplayAlert("Viga", "Sellise nimega pood on juba olemas", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(LocationEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisestage asukoht", "OK");
                    return;
                }

                // Parse coordinates (optional)
                double latitude = 0, longitude = 0;

                if (!string.IsNullOrWhiteSpace(LatitudeEntry.Text) &&
                    !double.TryParse(LatitudeEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out latitude))
                {
                    await DisplayAlert("Viga", "Vale laiuskraadi formaat", "OK");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(LongitudeEntry.Text) &&
                    !double.TryParse(LongitudeEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out longitude))
                {
                    await DisplayAlert("Viga", "Vale pikkuskraadi formaat", "OK");
                    return;
                }

                // Create new store
                var store = new Store
                {
                    Name = NameEntry.Text,
                    Location = LocationEntry.Text,
                    LogoFileName = LogoFileNameEntry.Text,
                    Latitude = latitude,
                    Longitude = longitude,
                    CreatedAt = DateTime.UtcNow
                };

                // Add store to database
                await _database.InsertStoreAsync(store);
                await DisplayAlert("Õnnestus", "Poe andmed on uuendatud", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Poe uuendamine ebaõnnestus: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
