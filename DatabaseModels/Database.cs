using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace toiduHind.DatabaseModels;
public class Database
{
    private readonly SQLiteAsyncConnection _db;

    public Database()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "users.db");
        Debug.WriteLine("ПУТЬ!!!!!!! " + dbPath);
        _db = new SQLiteAsyncConnection(dbPath);
        _db.CreateTableAsync<User>().Wait();
        _db.CreateTableAsync<Product>().Wait();
        _db.CreateTableAsync<Category>().Wait();
        _db.CreateTableAsync<Store>().Wait();
        _db.CreateTableAsync<Price>().Wait();
        _db.CreateTableAsync<Favorite>().Wait();
    }

    public async Task<bool> RegisterUser(User user)
    {
        var existingUser = await _db.Table<User>().Where(u => u.Email == user.Email).FirstOrDefaultAsync();
        if (existingUser != null) 
            return false;

        await _db.InsertAsync(user);
        return true;

    }


    public async Task<User?> GetUserByEmail(string email)
    {
        return await _db.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
    }


    public async Task<User> LoginUser(string email, string password)
    {
        return await _db.Table<User>().Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
    }

    public Task<int> UpdateUser(User user)
    {
        return _db.UpdateAsync(user);
    }

    public async Task ImportStoresFromCsvAsync()
    {
        try
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "stores.csv");
            if (!File.Exists(filePath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("stores.csv");
                using var dest = File.Create(filePath);
                await stream.CopyToAsync(dest);
            }

            using var reader = new StreamReader(filePath);
            var header = await reader.ReadLineAsync(); // Пропустить заголовок

            int countAdded = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var parts = line.Split(',');

                if (parts.Length < 3) continue;

                var store = new Store
                {
                    Name = parts[0].Trim(),
                    LogoFileName = parts[1].Trim(),
                    Location = parts[2].Trim()
                };

                var existingStore = await _db.Table<Store>().Where(s => s.Name == store.Name).FirstOrDefaultAsync();
                if (existingStore == null)
                {
                    await _db.InsertAsync(store);
                    countAdded++;
                }
            }

            await Application.Current.MainPage.DisplayAlert("Успех", $"{countAdded} магазинов импортировано.", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось импортировать магазины: {ex.Message}", "OK");
        }
    }



    public async Task AddStoreAsync(Store store)
    {
        var existing = await _db.Table<Store>().Where(s => s.Name == store.Name).FirstOrDefaultAsync();
        if (existing == null)
        {
            await _db.InsertAsync(store);
        }
    }

    public async Task AddUserAsync(User user)
    {
        var existingUser = await _db.Table<User>().Where(u => u.Email == user.Email).FirstOrDefaultAsync();
        if (existingUser == null)
        {
            await _db.InsertAsync(user);
        }
        else
        {
            throw new Exception("Пользователь с таким email уже существует.");
        }
    }


    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _db.Table<User>().ToListAsync();
    }

    public async Task DeleteUserByIdAsync(int userId)
    {
        var user = await _db.FindAsync<User>(userId);
        if (user != null)
        {
            await _db.DeleteAsync(user);
        }
    }


    public async Task<List<Store>> GetAllStoresAsync()
    {
        return await _db.Table<Store>().ToListAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _db.Table<Product>().ToListAsync();
    }

    public async Task DeleteStoreByIdAsync(int id)
    {
        var store = await _db.FindAsync<Store>(id);
        if (store != null)
            await _db.DeleteAsync(store);
    }

    public async Task DeleteProductByIdAsync(int id)
    {
        var product = await _db.FindAsync<Product>(id);
        if (product != null)
            await _db.DeleteAsync(product);
    }

    public async Task AddSampleProductsForStoresAsync()
    {
        var stores = await _db.Table<Store>().ToListAsync();
        var random = new Random();

        foreach (var store in stores)
        {
            for (int i = 0; i < 5; i++) 
            {
                var product = new Product
                {
                    Name = $"Продукт {i + 1} из {store.Name}",
                    Brand = $"Бренд {random.Next(1, 100)}",
                    CategoryId = random.Next(1, 10), 
                    ImageUrl = "default_product.png",
                    Description = $"Описание продукта {i + 1} из {store.Name}"
                };

                await _db.InsertAsync(product);

                var price = new Price
                {
                    ProductId = product.Id,
                    StoreId = store.Id,
                    CurrentPrice = random.Next(1, 100),
                    DiscountPrice = random.Next(1, 50),
                    StartDate = DateTime.UtcNow.AddDays(-random.Next(1, 10)),
                    EndDate = DateTime.UtcNow.AddDays(random.Next(1, 10))
                };

                await _db.InsertAsync(price);
            }
        }

        await Application.Current.MainPage.DisplayAlert("Успех", "Продукты добавлены для всех магазинов.", "OK");
    }

    public async Task<List<Price>> GetAllPricesAsync()
    {
        return await _db.Table<Price>().ToListAsync();
    }

    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _db.Table<Category>().Where(c => c.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        return await _db.Table<Product>().Where(p => p.CategoryId == categoryId).ToListAsync();
    }


    public async Task ImportCategoriesFromCsvAsync()
    {
        try
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "categories.csv");
            if (!File.Exists(filePath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("categories.csv");
                using var dest = File.Create(filePath);
                await stream.CopyToAsync(dest);
            }

            using var reader = new StreamReader(filePath);
            var header = await reader.ReadLineAsync(); // Пропустить заголовок

            int countAdded = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var parts = line.Split(',');

                if (parts.Length < 2) continue;

                var category = new Category
                {
                    Name = parts[0].Trim(),
                    ParentId = int.TryParse(parts[1].Trim(), out var parentId) ? parentId : (int?)null
                };

                var existingCategory = await _db.Table<Category>().Where(c => c.Name == category.Name).FirstOrDefaultAsync();
                if (existingCategory == null)
                {
                    await _db.InsertAsync(category);
                    countAdded++;
                }
            }

            await Application.Current.MainPage.DisplayAlert("Успех", $"{countAdded} категорий импортировано.", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось импортировать категории: {ex.Message}", "OK");
        }
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _db.Table<Category>().ToListAsync();
    }

    public async Task ImportProductsFromCsvAsync()
    {
        try
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, "products.csv");
            if (!File.Exists(filePath))
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("products.csv");
                using var dest = File.Create(filePath);
                await stream.CopyToAsync(dest);
            }

            using var reader = new StreamReader(filePath);
            var header = await reader.ReadLineAsync(); // Пропустить заголовок

            int countAdded = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var parts = line.Split(',');

                if (parts.Length < 7) continue;

                var product = new Product
                {
                    Name = parts[0].Trim(),
                    Brand = parts[1].Trim(),
                    CategoryId = int.Parse(parts[2].Trim()),
                    ImageUrl = parts[6].Trim(),
                    Description = $"Описание для {parts[0].Trim()}"
                };

                await _db.InsertAsync(product);

                var price = new Price
                {
                    ProductId = product.Id,
                    StoreId = int.Parse(parts[3].Trim()),
                    CurrentPrice = decimal.Parse(parts[4].Trim()),
                    DiscountPrice = string.IsNullOrWhiteSpace(parts[5]) ? (decimal?)null : decimal.Parse(parts[5].Trim()),
                    StartDate = DateTime.UtcNow.AddDays(-5),
                    EndDate = DateTime.UtcNow.AddDays(5)
                };

                await _db.InsertAsync(price);
                countAdded++;
            }

            await Application.Current.MainPage.DisplayAlert("Успех", $"{countAdded} товаров импортировано.", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось импортировать товары: {ex.Message}", "OK");
        }
    }


}

