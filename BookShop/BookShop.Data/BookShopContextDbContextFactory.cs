namespace BookShop.Data
{
    public sealed class BookShopContextDbContextFactory
    {
        private readonly string _connectionString;

        public BookShopContextDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BSContext GetContext()
        {
            return new BSContext(BookShopContextContextDesignTimeFactory.GetSqlServerOptions(_connectionString));
        }
    }
}

