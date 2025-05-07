using System;
using Microsoft.Maui.Controls;
using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.AdminPanels.EditDataFiles
{
    public partial class EditCategoryPage : ContentPage
    {
        private readonly Database _database;
        private readonly int _categoryId;
        private Category _category;
        private List<Category> _parentCategories;
        private string _originalName;

        public EditCategoryPage(int categoryId)
        {
            InitializeComponent();
            _database = new Database();
            _categoryId = categoryId;
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            try
            {
                // Load the category to edit
                _category = await _database.GetCategoryByIdAsync(_categoryId);
                if (_category == null)
                {
                    await DisplayAlert("Viga", "Kategooriat ei leitud", "OK");
                    await Navigation.PopAsync();
                    return;
                }

                _originalName = _category.Name;
                NameEntry.Text = _category.Name;

                // Check if this category has products, show warning if it does
                var productsInCategory = await _database.GetProductsByCategoryAsync(_categoryId);
                if (productsInCategory.Count > 0)
                {
                    WarningLabel.IsVisible = true;
                }

                // Load all categories for parent picker but exclude self and children
                _parentCategories = await _database.GetAllCategoriesAsync();
                _parentCategories = _parentCategories.Where(c => c.Id != _categoryId).ToList();

                // Remove any categories that have this category as parent (to avoid cycles)
                var childCategories = _parentCategories.Where(c => c.ParentId == _categoryId).ToList();
                foreach (var child in childCategories)
                {
                    _parentCategories.Remove(child);
                }

                // Add "None" option at the beginning
                var displayCategories = new List<string> { "Puudub (Peakategooria)" };
                displayCategories.AddRange(_parentCategories.Select(c => c.Name));

                ParentCategoryPicker.ItemsSource = displayCategories;

                // Set selected parent category or "None" if no parent
                if (_category.ParentId.HasValue)
                {
                    var parentIndex = _parentCategories.FindIndex(c => c.Id == _category.ParentId.Value);
                    if (parentIndex >= 0)
                    {
                        ParentCategoryPicker.SelectedIndex = parentIndex + 1; // +1 for "None" option
                    }
                    else
                    {
                        ParentCategoryPicker.SelectedIndex = 0;
                    }
                }
                else
                {
                    ParentCategoryPicker.SelectedIndex = 0; // None
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
                // Make sure we have a category loaded
                if (_category == null)
                {
                    await DisplayAlert("Viga", "Kategooria ei ole laaditud", "OK");
                    return;
                }

                // Validate input
                if (string.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    await DisplayAlert("Viga", "Palun sisesta kategooria nimi", "OK");
                    return;
                }

                // Check if name changed and if it already exists
                if (NameEntry.Text != _originalName)
                {
                    var existingCategory = await _database.GetCategoryByNameAsync(NameEntry.Text);
                    if (existingCategory != null && existingCategory.Id != _categoryId)
                    {
                        await DisplayAlert("Viga", "Sellise nimega kategooria on juba olemas", "OK");
                        return;
                    }
                }

                // Determine parent ID
                int? parentId = null;
                if (ParentCategoryPicker.SelectedIndex > 0) // Not "None"
                {
                    parentId = _parentCategories[ParentCategoryPicker.SelectedIndex - 1].Id;
                }

                // Update category
                _category.Name = NameEntry.Text;
                _category.ParentId = parentId;

                // Save to database
                await _database.UpdateCategoryAsync(_category);
                await DisplayAlert("Õnnestus", "Kategooria andmed on uuendatud", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Kategooria uuendamine ebaõnnestus: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
