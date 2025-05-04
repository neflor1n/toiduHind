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

    public double Latitude { get; set; } // Широта

    public double Longitude { get; set; } // Долгота

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Latitude и Longitude для хранения координат магазина.
}
