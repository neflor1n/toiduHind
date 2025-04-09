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

}

