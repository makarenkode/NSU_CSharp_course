﻿using BookShopSecond.Data;
using ContractLibrary.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Services
{
    public class BookService
    {
        private readonly BookShopContextDbContextFactory _dbContextFactory;
        private double MinBookPercent = 0.1;

        public BookService(BookShopContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            InitShop();
        }
        public BookService()
        {

        }

        public void InitShop()
        {
            var bsContext = _dbContextFactory.GetContext();
            if (!bsContext.ShopAny())
            {
                var shop = new Shop
                {
                    Balance = 1000,
                    MaxBookQuantity = 100
                };
                 bsContext.AddShop(shop);
            }

        }

        public Book CreateBook(String name, String genre, decimal price, bool isNew, DateTime dateOfDelivery)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = name,
                Genre = genre,
                Price = price,
                IsNew = isNew,
                DateOfDelivery = dateOfDelivery
            };
            return book;
        }
        public Book JsonToBook(JsonBook jsonBook)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = jsonBook.Title,
                Genre = jsonBook.Genre,
                Price = jsonBook.Price,
                IsNew = jsonBook.IsNew,
                DateOfDelivery = jsonBook.DateOfDelivery
            };
            return book;
        }

#warning неиспользуемый метод
#warning используется в контроллере для получения одной книги
#warning нет, не используется) 
#warning да не используется, убрал)


        public virtual async Task<List<Book>> GetBooks()
        {
            var list = new List<Book>();
            foreach(var book in await _dbContextFactory.GetContext().GetBooks())
            {
                list.Add(book);
            }
            return list;
        }

        public async Task MakeDiscount(string genre, decimal discount)
        {
            var bsContext = _dbContextFactory.GetContext();
            foreach (var book in await bsContext.GetBooks())
            {
                if(book.Genre == genre)
                {
                    book.Price *= discount;
                    await bsContext.UpdateBook(book);
                }
            }
        }
        public async Task SellAllBook()
        {
            foreach(var b in await _dbContextFactory.GetContext().GetBooks())
            {
                await SellBook(b.Id);
            }
        }
        public async Task SellBook(Guid id)
        {
            var bsContext = _dbContextFactory.GetContext();
            var book = await bsContext.GetBook(id);
            var shop = await bsContext.GetShop(1);
            shop.Balance += book.Price;
            var trans = await _dbContextFactory.GetContext().Database.BeginTransactionAsync();
            try
            {
                await bsContext.UpdateShop(shop);
                await bsContext.DeleteBook(book);
                await trans.CommitAsync();
            }
#warning ох, очень плохой код. у тебя будет отловлен абсолютно любой exception, а ты, во-первых, не узнаешь какой, а во-вторых - ничего не сделаешь
#warning исправил
            catch (Exception ex)
            {
                Console.WriteLine("Error in SellBook:" + ex.Message);
                trans.Rollback();
            }

        }

        #warning в названиях методов обычно опускают слово async. что метод асинхронный сигнализирует возвращаемый тип - Task, Task<T>
        public async Task AddBook(Book book)
        {
            var bsContext = _dbContextFactory.GetContext();
            var shop = await _dbContextFactory.GetContext().GetShop(1);
            if (shop.MaxBookQuantity <= await _dbContextFactory.GetContext().BooksCount())
            {
                return;
            }
            await bsContext.AddBook(book);
            if (shop.Balance > book.Price * 0.07M) 
            {
                shop.Balance -= book.Price * 0.07M;
                await bsContext.UpdateShop(shop);
            }
        }

        public async Task<bool> NeedBooks()
        {
#warning слишком жёстко. ну _dbContextFactory.GetContext() уж точно можно было вынести в переменную
#warning исправил
            var context = _dbContextFactory.GetContext();
            var shop = await _dbContextFactory.GetContext().GetShop(1);
            var bookQuantity = await context.BooksCount();
            if (bookQuantity < MinBookPercent * shop.MaxBookQuantity)
            {
                return true;
            }
            return false;
        }
    }
}
