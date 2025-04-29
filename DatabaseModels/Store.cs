using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace toiduHind.DatabaseModels;

public class Store
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull]
    public string Name { get; set; }
    public string Location { get; set; }
    public string LogoFileName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
