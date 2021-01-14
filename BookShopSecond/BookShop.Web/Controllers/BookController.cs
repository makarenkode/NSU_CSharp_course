using BookShop.Web.Services;
using BookShopSecond.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Web.Controllers
{
    [Route("books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController (BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<List<Book>> GetBooksAsync()
        {
           return await _bookService.GetBooksAsync();
        }

        [HttpGet, Route("sell/{id}")]
        public async Task<List<Book>> SellBookAsync(Guid id)
        {
            await _bookService.SellBookAsync(id);
            return await GetBooksAsync();

        }

        [HttpGet, Route("sell/all")]
        public async Task<List<Book>> SellAllAsync()
        {
            await _bookService.SellAllBookAsync();
            return await GetBooksAsync();
        }
        [HttpGet, Route("discount/{genre}&{discount}")]
        public async Task<List<Book>> MakeDiscountAsync(string genre, decimal discount)
        {
            await _bookService.MakeDiscountAsync(genre, discount);
            return await GetBooksAsync();
        }
    }
}
