using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace toiduHind.DatabaseModels;

public class Product
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Name { get; set; }

    public string Brand { get; set; }

    [NotNull]
    public int CategoryId { get; set; }

    public string ImageUrl { get; set; }

    public string Description { get; set; }

    public int PopularityScore { get; set; } = 0; // Рейтинг популярности // PopularityScore для сортировки продуктов по популярности.

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
