using System.Threading.Tasks;
using BookShop.Web.Services;
using BookShopSecond.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers
{
    [Route("shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }
        
        [HttpGet]
        public async Task<Shop> GetShop()
        {
           return await  _shopService.GetShop();
        }
    }
}
