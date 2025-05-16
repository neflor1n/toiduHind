using toiduHind.DatabaseModels;

namespace toiduHind.MainToiduFol.HomePage
{
    public partial class HomePage : ContentPage
    {
        private bool isMenuVisible = false;
        private const int menuWidth = 250;
        private readonly User _loggedInUser;
        private string _currentCategoryName;
        private List<object> _allProducts;
        private const string TAG = "HomePage";

        private Dictionary<string, List<object>> _categorizedProducts = new Dictionary<string, List<object>>();

        public HomePage(User user)
        {
            InitializeComponent();
            _loggedInUser = user;

            SetUserGreeting();

            SlidingMenu.TranslationX = -menuWidth;
            Overlay.IsVisible = false;
            Overlay.InputTransparent = true;

            LoadInitialDataAsync();
        }

        private async void LoadInitialDataAsync()
        {
            try
            {
                // Загружаем первую категорию из списка
                var categories = await App.Database.GetAllCategoriesAsync();
                await LoadDiscountsAsync();

                if (categories != null && categories.Count > 0)
                {
                    await LoadProductsByCategoryAsync(categories[0].Name);
                }
                else
                {
                    Console.WriteLine($"{TAG}: No categories found to load initial data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{TAG}: Error loading initial data: {ex.Message}");
            }
        }

        private void SetUserGreeting()
        {
            if (!string.IsNullOrEmpty(_loggedInUser?.Name))
                GreetingLabel.Text = $"Tere, {_loggedInUser.Name}";
            else
                GreetingLabel.Text = "Tere!";
        }

        private async void OnMenuButtonClicked(object sender, EventArgs e)
        {
            if (isMenuVisible)
                await HideMenu();
            else
                await ShowMenu();
        }

        private async Task ShowMenu()
        {
            Overlay.IsVisible = true;
            Overlay.InputTransparent = false;
            await Overlay.FadeTo(0.5, 250);
            await SlidingMenu.TranslateTo(0, 0, 250, Easing.CubicInOut);
            isMenuVisible = true;
        }

        private async Task HideMenu()
        {
            await SlidingMenu.TranslateTo(-menuWidth, 0, 250, Easing.CubicInOut);
            await Overlay.FadeTo(0, 250);
            Overlay.IsVisible = false;
            Overlay.InputTransparent = true;
            isMenuVisible = false;
        }

        private async void OnOptionSelected(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            string categoryName = button.Text.Trim();

            ProductSearchBar.Text = string.Empty;

            await LoadProductsByCategoryAsync(categoryName);

            await HideMenu();
        }

        private async Task LoadProductsByCategoryAsync(string categoryName, bool updateSearch = true)
        {
            _currentCategoryName = categoryName;

            var category = await App.Database.GetCategoryByNameAsync(categoryName);
            if (category == null)
            {
                await DisplayAlert("Viga", "Kategooriat ei leitud.", "OK");
                return;
            }

            // Проверяем, загружали ли мы уже эту категорию
            if (_categorizedProducts.ContainsKey(categoryName.ToLowerInvariant()))
            {
                Console.WriteLine($"{TAG}: Loading {categoryName} from cache");
                _allProducts = _categorizedProducts[categoryName.ToLowerInvariant()];

                if (updateSearch && !string.IsNullOrWhiteSpace(ProductSearchBar?.Text))
                {
                    FilterProducts(ProductSearchBar.Text);
                }
                else
                {
                    ProductsCollection.ItemsSource = _allProducts;
                }
                return;
            }

            var products = await App.Database.GetProductsByCategoryAsync(category.Id);
            var prices = await App.Database.GetAllPricesAsync();
            var stores = await App.Database.GetAllStoresAsync();

            // Создаем список вариантов продуктов с информацией о магазине и цене
            var productVariants = products.Select(product =>
            {
                var price = prices.FirstOrDefault(p => p.ProductId == product.Id);
                var store = stores.FirstOrDefault(s => s.Id == price?.StoreId);

                return new
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductNameLower = product.Name.ToLowerInvariant(),
                    Brand = product.Brand,
                    StoreId = store?.Id ?? 0,
                    StoreName = store?.Name ?? "Tundmatu pood",
                    Price = price?.CurrentPrice ?? 0,
                    Discount = price?.DiscountPrice
                };
            })
            .GroupBy(v => new { NameLower = v.ProductNameLower, v.Brand, v.StoreId })
            .Select(g => g.First())
            .ToList();

            // Группируем по названию товара (без учета регистра)
            var groupedProducts = productVariants
                .GroupBy(v => v.ProductNameLower)
                .Select(group => new
                {
                    ProductName = group.First().ProductName,
                    Variants = group.OrderBy(v => v.Price).ToList()
                })
                .OrderBy(g => g.ProductName)
                .ToList();

            // Сохраняем для текущей категории
            _allProducts = new List<object>(groupedProducts);
            _categorizedProducts[categoryName.ToLowerInvariant()] = _allProducts;

            if (updateSearch && !string.IsNullOrWhiteSpace(ProductSearchBar?.Text))
            {
                FilterProducts(ProductSearchBar.Text);
            }
            else
            {
                ProductsCollection.ItemsSource = _allProducts;
            }
        }





        private async Task SearchAllCategoriesAsync(string searchText)
        {
            Console.WriteLine($"{TAG}: Searching all categories for '{searchText}'");

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Если поиск пустой, просто отображаем текущую категорию
                ProductsCollection.ItemsSource = _allProducts;
                return;
            }

            var categories = await App.Database.GetAllCategoriesAsync();

            // Для каждой категории, которую мы еще не загружали, загружаем данные
            foreach (var category in categories)
            {
                if (!_categorizedProducts.ContainsKey(category.Name.ToLowerInvariant()))
                {
                    await LoadProductsByCategoryAsync(category.Name, false);
                }
            }

            // Ищем среди всех загруженных категорий
            List<object> allResults = new List<object>();
            searchText = searchText.ToLowerInvariant();

            foreach (var categoryProducts in _categorizedProducts.Values)
            {
                foreach (dynamic product in categoryProducts)
                {
                    bool nameMatches = ((string)product.ProductName).ToLowerInvariant().Contains(searchText);
                    bool variantMatches = false;

                    foreach (dynamic variant in product.Variants)
                    {
                        if (((string)variant.Brand).ToLowerInvariant().Contains(searchText) ||
                            ((string)variant.StoreName).ToLowerInvariant().Contains(searchText))
                        {
                            variantMatches = true;
                            break;
                        }
                    }

                    if (nameMatches || variantMatches)
                    {
                        allResults.Add(product);
                    }
                }
            }

            Console.WriteLine($"{TAG}: Found {allResults.Count} matches across all categories");
            ProductsCollection.ItemsSource = allResults;
        }

        private async void OnOverlayTapped(object sender, EventArgs e)
        {
            await HideMenu();
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await HideMenu();
            await Navigation.PushAsync(new SettingsPage.SettingsPage(_loggedInUser));
        }

        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            string searchText = ProductSearchBar.Text;
            FilterProducts(searchText);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            Console.WriteLine($"{TAG}: SearchText changed to '{searchText}'");

            // Если поисковый запрос пустой, показываем текущую категорию
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Console.WriteLine($"{TAG}: SearchText is empty, showing current category");
                ProductsCollection.ItemsSource = _allProducts;
                return;
            }

            // Для лучшего пользовательского опыта, фильтруем при вводе
            FilterProducts(searchText);
        }
        private async void FilterProducts(string searchText)
        {
            // Если у нас уже загружено несколько категорий или текст поиска не пустой,
            // используем поиск по всем категориям
            if (_categorizedProducts.Count > 1 || !string.IsNullOrWhiteSpace(searchText))
            {
                await SearchAllCategoriesAsync(searchText);
                return;
            }

            Console.WriteLine($"{TAG}: FilterProducts called with '{searchText}'");

            if (_allProducts == null)
            {
                Console.WriteLine($"{TAG}: _allProducts is null");
                return;
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                Console.WriteLine($"{TAG}: searchText is empty, showing all products");
                ProductsCollection.ItemsSource = _allProducts;
                return;
            }

            searchText = searchText.ToLowerInvariant();
            List<object> filteredProducts = new List<object>();
            int countMatches = 0;

            foreach (dynamic product in _allProducts)
            {
                bool nameMatches = ((string)product.ProductName).ToLowerInvariant().Contains(searchText);
                bool variantMatches = false;

                foreach (dynamic variant in product.Variants)
                {
                    if (((string)variant.Brand).ToLowerInvariant().Contains(searchText) ||
                        ((string)variant.StoreName).ToLowerInvariant().Contains(searchText))
                    {
                        variantMatches = true;
                        break;
                    }
                }

                if (nameMatches || variantMatches)
                {
                    countMatches++;
                    filteredProducts.Add(product);
                }
            }

            Console.WriteLine($"{TAG}: Found {countMatches} matches in current category");
            ProductsCollection.ItemsSource = filteredProducts;
        }


        private async Task LoadDiscountsAsync()
        {
            var allPrices = await App.Database.GetAllPricesAsync();
            var allProducts = await App.Database.GetAllProductsAsync();
            var allStores = await App.Database.GetAllStoresAsync();

            var discountedItems = allPrices
                .Where(p => p.DiscountPrice != null && p.DiscountPrice < p.CurrentPrice)
                .Select(p =>
                {
                    var product = allProducts.FirstOrDefault(prod => prod.Id == p.ProductId);
                    var store = allStores.FirstOrDefault(s => s.Id == p.StoreId);
                    return new
                    {
                        ProductName = product?.Name ?? "Tundmatu toode",
                        Brand = product?.Brand ?? "",
                        StoreName = store?.Name ?? "Tundmatu pood",
                        Price = p.CurrentPrice,
                        Discount = p.DiscountPrice
                    };
                })
                .ToList();

            DiscountsCollection.ItemsSource = discountedItems;
        }


    }
}
