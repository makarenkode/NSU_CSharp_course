using BookShopSecond.Data;
using ContractLibrary.JsonModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Web.Services
{
    public class BookService
    {
        private readonly BookShopContextDbContextFactory _dbContextFactory;
        public const double MinBookPercent = 0.1;

        public BookService(BookShopContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            InitShop();
        }

        public void InitShop()
        {
            var bsContext = _dbContextFactory.GetContext();
            if (bsContext.ShopAny()) return;
            var shop = new Shop
            {
                Balance = 1000,
                MaxBookQuantity = 100
            };
            bsContext.AddShop(shop);

        }

#warning ррррр
#warning убрал неиспользуемый код
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

        public virtual async Task<List<Book>> GetBooks()
        {
            var list = new List<Book>();
            foreach(var book in await _dbContextFactory.GetContext().GetBooks())
            {
                list.Add(book);
            }
            return list;
        }

        public async Task<Guid> GetBookId(string genre)
        {
            var book = await _dbContextFactory.GetContext().GetBook(genre);
            return book.Id;
        }
        public async Task<Guid> GetBookId()
        {
            var book = await _dbContextFactory.GetContext().GetBook();
            return book.Id;
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
        public async Task<Book> SellBook(Guid id)
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
            catch (Exception ex)
            {
                Console.WriteLine("Error in SellBook:" + ex.Message);
                await trans.RollbackAsync();
            }

            return book;
        }

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
            var context = _dbContextFactory.GetContext();
            var shop = await _dbContextFactory.GetContext().GetShop(1);
            var bookQuantity = await context.BooksCount();
            return bookQuantity < MinBookPercent * shop.MaxBookQuantity;
        }
    }
}
