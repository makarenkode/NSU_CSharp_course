using BookShop.Web.Services;
using BookShopSecond.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Web.Controllers
{
    [Route("shop")]
    [ApiController]
    public class ShopController
    {
        private readonly ShopService _shopService;

        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }
        
        [HttpGet]
        public Shop GetShop()
        {
           return  _shopService.GetShop();
        }
    }
}
