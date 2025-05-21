using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toiduHind.DatabaseModels
{
    public class ProductVariantModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string StoreName { get; set; }
        public int StoreId { get; set; }

        public decimal Price { get; set; }
        public decimal? Discount { get; set; }

        public string ProductNameLower => ProductName.ToLowerInvariant();
    }
}
