using FluentAssertions;
using NUnit.Framework;
using System;
using BookShop.Data;

namespace BookShop.Test
{
    [TestFixture]
    public class InitDataTests
    {

        [Test]
        public void BookInitTest()
        {
            var id = Guid.NewGuid();
            var title = "Important years";
            var price = 100;
            var genre = "Motivation";
            var date = DateTime.Now;
            var book = new Book
            {
                Id = id,
                Title = title,
                Price = price,
                Genre = genre,
                DateOfDelivery = date,
                IsNew = true

            };
            book.Title.Should().Be(title);
            book.Price.Should().Be(price);
            book.DateOfDelivery.Should().Be(date);
        }

        [Test]
        public void ShopInitTest()
        {
            var balance = 1000;
            var maxBookQuantity = 100;
            var shop = new Shop
            {
                Id = 1,
                Balance = balance,
                MaxBookQuantity = maxBookQuantity
            };
            shop.Id.Should().Be(1);
            shop.Balance.Should().Be(balance);
            shop.MaxBookQuantity.Should().Be(maxBookQuantity);
        }
    }
}