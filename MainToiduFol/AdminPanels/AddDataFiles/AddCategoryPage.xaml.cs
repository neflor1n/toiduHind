using System;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels.AddDataFiles
{
    public partial class AddCategoryPage : ContentPage
    {
        private readonly Database _database;
        private List<Category> _parentCategories;

        public AddCategoryPage()
        {
            InitializeComponent();
            _database = new Database();
            LoadParentCategoriesAsync();
        }

        private async void LoadParentCategoriesAsync()
        {
            try
            {
                // Get all categories to show in parent picker
                _parentCategories = await _database.GetAllCategoriesAsync();

                // Add "None" option at the beginning
                var displayCategories = new List<string> { "None (Top-level category)" };
                displayCategories.AddRange(_parentCategories.Select(c => c.Name));

                ParentCategoryPicker.ItemsSource = displayCategories;
                ParentCategoryPicker.SelectedIndex = 0; // Default to "None"
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Vanemate kategooriate laadimise viga: {ex.Message}", "OK");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisesta kategooria nimi", "OK");
                    return;
                }

                // Check if category with this name already exists
                var existingCategory = await _database.GetCategoryByNameAsync(NameEntry.Text);
                if (existingCategory != null)
                {
                    await DisplayAlert("Viga", "Sellise nimega kategooria on juba olemas", "OK");
                    return;
                }

                // Determine parent ID
                int? parentId = null;
                if (ParentCategoryPicker.SelectedIndex > 0) // Not "None"
                {
                    parentId = _parentCategories[ParentCategoryPicker.SelectedIndex - 1].Id;
                }

                // Create new category
                var category = new Category
                {
                    Name = NameEntry.Text,
                    ParentId = parentId
                };

                // Save to database
                await _database.InsertCategoryAsync(category);
                await DisplayAlert("Õnnestus", "Kategooria on edukalt lisatud", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Kategooria lisamine ebaõnnestus: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
