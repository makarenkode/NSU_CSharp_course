using System;
using System.ComponentModel.DataAnnotations;


namespace BookShopSecond.Data
{
    public class Book
    {
        //#warning пожалуйста, никогда не делай так конфигурацию сущностей) 
        //#warning пересмотри лекцию про EF, посмотри как там сделана конфигурация сущностей 

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public bool IsNew { get; set; }
        public DateTime DateOfDelivery { get; set; }
    }
}
