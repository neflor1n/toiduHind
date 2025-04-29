using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class StoresPage : ContentPage
{
    public StoresPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        StoresCollection.ItemsSource = await App.Database.GetAllStoresAsync();
    }

    private async void OnDeleteStoreClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        int storeId = (int)button.CommandParameter;

        bool confirm = await DisplayAlert("Kustuta", "Kas oled kindel?", "Jah", "Ei");
        if (!confirm) return;

        await App.Database.DeleteStoreByIdAsync(storeId);
        StoresCollection.ItemsSource = await App.Database.GetAllStoresAsync();
    }

}
