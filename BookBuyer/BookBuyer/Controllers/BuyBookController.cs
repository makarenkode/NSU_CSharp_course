using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookBuyer.Services;

namespace BookBuyer.Controllers
{
    [Route("books")]
    [ApiController]
    public class BuyBookController : ControllerBase
    {
        private readonly BuyService _buyService;

        public BuyBookController(BuyService buyService)
        {
            _buyService = buyService;
        }

        [HttpGet]
        public async Task<List<Book>> GetBooks()
        {
            return await _buyService.GetBooks();
        }

        [HttpGet, Route("{id}")]
        public async Task<Book> BuyBook(Guid id)
        {
            return await _buyService.BuyBook(id);
        }

        [HttpGet, Route("get/{genre}")]
        public async Task<Guid> GetBookId(string genre)
        {
            return await _buyService.GetBookId(genre);
        }

        [HttpGet, Route("get/")]
        public async Task<Guid> GetBookId()
        {
            return await _buyService.GetBookId();
        }

    }
}
