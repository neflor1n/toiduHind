using toiduHind.DatabaseModels;
using toiduHind.MainToiduFol.AdminPanels.AddDataFiles;
using toiduHind.MainToiduFol.AdminPanels.EditDataFiles;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class StoresPage : ContentPage
{
    private Database _database;

    public StoresPage()
    {
        InitializeComponent();
        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadStoresAsync();
    }

    private async Task LoadStoresAsync()
    {
        StoresCollection.ItemsSource = await _database.GetAllStoresAsync();
    }

    private async void OnDeleteStoreClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int storeId)
        {
            bool confirm = await DisplayAlert("Kustutamine", "Kas olete kindel, et soovite selle poe kustutada?", "Jah", "Ei");
            if (confirm)
            {
                try
                {
                    await _database.DeleteStoreByIdAsync(storeId);
                    await LoadStoresAsync();
                    await DisplayAlert("Õnnestus", "Pood on kustutatud.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Viga", $"Poe kustutamine ebaõnnestus: {ex.Message}", "OK");
                }
            }
        }
    }

    private async void OnAddStoreClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddStorePage());
    }

    private async void OnEditStoreClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int storeId)
        {
            await Navigation.PushAsync(new EditStorePage(storeId));
        }
    }

    private async void OnStoreTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is int storeId)
        {
            try
            {
                // Get the store details
                var store = await _database.GetStoreByIdAsync(storeId);
                if (store != null)
                {
                    // Show store details
                    string details = $"ID: {store.Id}\n" +
                                    $"Name: {store.Name}\n" +
                                    $"Location: {store.Location}\n" +
                                    $"Coordinates: {store.Latitude}, {store.Longitude}\n" +
                                    $"Logo: {store.LogoFileName}\n" +
                                    $"Created: {store.CreatedAt:g}";

                    await DisplayAlert($"Store Details: {store.Name}", details, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load store details: {ex.Message}", "OK");
            }
        }
    }
}
