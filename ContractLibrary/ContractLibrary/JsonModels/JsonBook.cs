using System;

namespace ContractLibrary.JsonModels
{
    public class JsonBook
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Genre { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}
