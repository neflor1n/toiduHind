using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace toiduHind.DatabaseModels;

public class BasketItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Brand { get; set; }
    public string StoreName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    [Ignore]
    public string TotalPriceFormatted => $"Hind: {(Price * Quantity):0.00} €";

}
