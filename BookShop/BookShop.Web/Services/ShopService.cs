using System.Threading.Tasks;
using BookShop.Data;

namespace BookShop.Web.Services
{
    public class ShopService
    {
        private readonly BookShopContextDbContextFactory _dbContextFactory;

        public ShopService(BookShopContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Shop> GetShop()
        {
            return  await _dbContextFactory.GetContext().GetShop(1);
            
        }
    }
}
