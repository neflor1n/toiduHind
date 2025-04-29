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
            using var stream = await FileSystem.OpenAppPackageFileAsync("estonian_stores.csv");
            using var reader = new StreamReader(stream);
            var header = await reader.ReadLineAsync(); // пропустить заголовок

            int countAdded = 0;
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var parts = line.Split(',');

                if (parts.Length < 3)
                    continue;

                string name = parts[0].Trim();
                string logoFileName = parts[1].Trim();
                string location = parts[2].Trim();

                // Проверка на дубликат по названию
                var existing = await _db.Table<Store>()
                    .Where(s => s.Name.ToLower() == name.ToLower())
                    .FirstOrDefaultAsync();

                if (existing == null)
                {
                    var store = new Store
                    {
                        Name = name,
                        LogoFileName = logoFileName,
                        Location = location,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _db.InsertAsync(store);
                    countAdded++;
                }
            }

            await Application.Current.MainPage.DisplayAlert("Impordi tulemus", $"{countAdded} uut poodi lisatud.", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Viga", $"Import ebaõnnestus: {ex.Message}", "OK");
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


}

