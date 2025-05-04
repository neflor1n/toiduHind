using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class CategoriesPage : ContentPage
{
    public CategoriesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await App.Database.GetAllCategoriesAsync();
        CategoriesCollection.ItemsSource = categories;
    }

    private async void OnImportCategoriesClicked(object sender, EventArgs e)
    {
        await App.Database.ImportCategoriesFromCsvAsync();
        await LoadCategoriesAsync();
    }
}
