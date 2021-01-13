using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopSecond.Data
{
    public class Shop
    {
        public int id { get; set; }
        public decimal Balance { get; set; }
        public int MaxBookQuantity { get; set; }

    }
}
