using toiduHind.DatabaseModels;
using toiduHind.MainToiduFol.AdminPanels.AddDataFiles;
using toiduHind.MainToiduFol.AdminPanels.EditDataFiles;

namespace toiduHind.MainToiduFol.AdminPanels;

public partial class CategoriesPage : ContentPage
{
    private Database _database;

    public CategoriesPage()
    {
        InitializeComponent();
        _database = new Database();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadCategoriesAsync();
    }

    private async Task LoadCategoriesAsync()
    {
        var categories = await _database.GetAllCategoriesAsync();
        CategoriesCollection.ItemsSource = categories;
    }

    private async void OnAddCategoryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCategoryPage());
    }

    private async void OnEditCategoryClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int categoryId)
        {
            await Navigation.PushAsync(new EditCategoryPage(categoryId));
        }
    }

    private async void OnDeleteCategoryClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.CommandParameter is int categoryId)
        {
            bool confirm = await DisplayAlert("Удаление", "Вы уверены, что хотите удалить эту категорию?", "Да", "Нет");
            if (confirm)
            {
                try
                {
                    // Check if there are products using this category
                    var productsInCategory = await _database.GetProductsByCategoryAsync(categoryId);
                    if (productsInCategory.Count > 0)
                    {
                        bool confirmWithProducts = await DisplayAlert(
                            "Внимание!",
                            $"В этой категории есть {productsInCategory.Count} продуктов. Удаление категории повлияет на эти продукты. Продолжить?",
                            "Да", "Нет");

                        if (!confirmWithProducts)
                            return;
                    }

                    // Check if there are subcategories
                    var subcategories = await _database.GetSubcategoriesAsync(categoryId);
                    if (subcategories.Count > 0)
                    {
                        bool confirmWithSubcategories = await DisplayAlert(
                            "Внимание!",
                            $"У этой категории есть {subcategories.Count} подкатегорий. Удаление повлияет на структуру категорий. Продолжить?",
                            "Да", "Нет");

                        if (!confirmWithSubcategories)
                            return;
                    }

                    await _database.DeleteCategoryByIdAsync(categoryId);
                    await LoadCategoriesAsync();
                    await DisplayAlert("Успех", "Категория удалена.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Не удалось удалить категорию: {ex.Message}", "OK");
                }
            }
        }
    }

}
