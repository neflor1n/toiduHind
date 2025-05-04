using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace toiduHind.DatabaseModels;

public class Price
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public int ProductId { get; set; }

    [NotNull]
    public int StoreId { get; set; }

    [NotNull]
    public decimal CurrentPrice { get; set; }

    public decimal? DiscountPrice { get; set; }

    public bool IsDiscounted => DiscountPrice.HasValue && DiscountPrice < CurrentPrice; // IsDiscounted для быстрого определения, является ли цена скидочной.

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}


