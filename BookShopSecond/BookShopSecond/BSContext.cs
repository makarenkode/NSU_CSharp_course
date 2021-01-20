using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShopSecond.Data
{
    public sealed class BSContext : DbContext
    {
        public const string DefaultSchemaName = "BookShop";
        private const string DefaultConnectionString = "Server=mybookshopserver.database.windows.net;Database=BookShop; User Id=d.makarenko; Password=Danil19081999;";
        public BSContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public BSContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DefaultConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
        }
		public async Task<List<Book>> GetBooks()
		{
			return await Set<Book>()
				.ToListAsync();
		}
        public async Task<int> BooksCount()
        {
            return await Set<Book>().CountAsync();
        }

        public async Task<Book> GetBook(Guid id)
        {
            return await Set<Book>().FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Book> GetBook(string genre)
        {
            return await Set<Book>().FirstOrDefaultAsync(b => b.Genre == genre);
        }
        public async Task<Book> GetBook()
        {
            var rand = new Random();
            var quantity = await BooksCount();
            var index = rand.Next(quantity);
            var books = await GetBooks();
            var counter = 0;
            foreach (var book in books)
            {
                counter++;
                if (counter == index)
                {
                    return book;
                }

            }

            return await Set<Book>().FirstOrDefaultAsync();
        }
        public async Task<Shop> GetShop(int id)
        {
            return await Set<Shop>().FirstOrDefaultAsync(b => b.Id == id);
        }

        public bool ShopAny()
        {
            return Set<Shop>().Local.Count != 0;
        }


        public async Task<Book> AddBook(Book book)
        {
            await Set<Book>().AddAsync(book);
            await SaveChangesAsync();
            return book;
        }
        public Shop AddShop(Shop shop)
        {
            Set<Shop>().Add(shop);
            SaveChanges();
            return shop;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            Set<Book>().Update(book);
            await SaveChangesAsync();
            return book;
        }
        public async Task<Shop> UpdateShop(Shop shop)
        {
            Set<Shop>().Update(shop);
            await SaveChangesAsync();
            return shop;
        }

        public async Task DeleteBook(Book book)
        {
            Set<Book>().Remove(book);
            await SaveChangesAsync();
        }
    }
}
