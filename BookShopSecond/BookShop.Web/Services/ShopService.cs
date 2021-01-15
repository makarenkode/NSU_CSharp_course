using BookShopSecond.Data;

namespace BookShop.Web.Services
{
    public class ShopService
    {
        private readonly BookShopContextDbContextFactory _dbContextFactory;

        public ShopService(BookShopContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Shop GetShop()
        {
            var shop = _dbContextFactory.GetContext().GetShop(1);
            return shop.Result;
        }
#warning неиспользуемый метод
#warning используется в BookService при продаже книги(метод SellBook)
#warning  http://prntscr.com/wnce9o
#warning да, убрал не нужный метод)
    }
}
