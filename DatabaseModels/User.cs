using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace toiduHind.DatabaseModels;
public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique, NotNull]
    public string Email { get; set; }

    [NotNull]
    public string Name { get; set; }

    [NotNull]
    public string Password { get; set; }

    [NotNull]
    public string Role { get; set; } = "User"; 
}

