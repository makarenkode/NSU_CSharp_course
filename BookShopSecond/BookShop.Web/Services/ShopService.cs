using BookShopSecond.Data;
using System.Threading.Tasks;

namespace BookShop.Web.Services
{
    public class ShopService
    {
        private readonly BookShopContextDbContextFactory _dbContextFactory;

        public ShopService(BookShopContextDbContextFactory dbContextFactory)
        {
            //#warning контекст лучше всё-таки получать непосредственно перед обращением к нему
            //#warning исправил
            _dbContextFactory = dbContextFactory;
        }

        public Shop GetShop()
        {
            var shop = _dbContextFactory.GetContext().GetShop(1);
            return shop.Result;
        }
        //#warning неиспользуемый метод
        //#warning используется в BookService при продаже книги(метод SellBook)
        public async Task AddMoneyAsync (decimal money)
        {
            var bsContext = _dbContextFactory.GetContext();
            var shop = bsContext.GetShop(1);
            shop.Result.Balance += money;
            await bsContext.UpdateShop(shop.Result);
        }
    }
}
