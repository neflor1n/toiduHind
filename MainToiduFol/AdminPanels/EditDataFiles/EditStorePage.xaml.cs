using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels.EditDataFiles
{
    public partial class EditStorePage : ContentPage
    {
        private readonly Database _database;
        private readonly int _storeId;
        private Store _store;
        private string _originalName;

        public EditStorePage(int storeId)
        {
            InitializeComponent();
            _database = new Database();
            _storeId = storeId;
            LoadStoreDataAsync();
        }

        private async void LoadStoreDataAsync()
        {
            try
            {
                _store = await _database.GetStoreByIdAsync(_storeId);
                if (_store == null)
                {
                    await DisplayAlert("Viga", "Poodi ei leitud", "OK");
                    await Navigation.PopAsync();
                    return;
                }

                // Save original name for later comparison
                _originalName = _store.Name;

                // Populate fields with store data
                NameEntry.Text = _store.Name;
                LocationEntry.Text = _store.Location;
                LogoFileNameEntry.Text = _store.LogoFileName;
                LatitudeEntry.Text = _store.Latitude.ToString(CultureInfo.InvariantCulture);
                LongitudeEntry.Text = _store.Longitude.ToString(CultureInfo.InvariantCulture);
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
                // Make sure we have a store loaded
                if (_store == null)
                {
                    await DisplayAlert("Viga", "Pood ei ole laaditud", "OK");
                    return;
                }

                // Validate inputs
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisestage poe nimi", "OK");
                    return;
                }

                // Check if store name changed and if it already exists
                if (NameEntry.Text != _originalName)
                {
                    var existingStore = await _database.GetStoreByNameAsync(NameEntry.Text);
                    if (existingStore != null && existingStore.Id != _storeId)
                    {
                        await DisplayAlert("Viga", "Sellise nimega pood on juba olemas", "OK");
                        return;
                    }
                }

                if (string.IsNullOrWhiteSpace(LocationEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisestage asukoht", "OK");
                    return;
                }

                // Parse coordinates
                if (!double.TryParse(LatitudeEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double latitude))
                {
                    await DisplayAlert("Viga", "Vale laiuskraadi formaat", "OK");
                    return;
                }

                if (!double.TryParse(LongitudeEntry.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double longitude))
                {
                    await DisplayAlert("Viga", "Vale pikkuskraadi formaat", "OK");
                    return;
                }

                // Update store data
                _store.Name = NameEntry.Text;
                _store.Location = LocationEntry.Text;
                _store.LogoFileName = LogoFileNameEntry.Text;
                _store.Latitude = latitude;
                _store.Longitude = longitude;

                // Save to database
                await _database.UpdateStoreAsync(_store);
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
