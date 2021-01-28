using BookShop.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShop.Data;

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
        public async Task<List<Book>> GetBooks()
        {
           return await _bookService.GetBooks();
        }

        [HttpGet, Route("get/{genre}")]
        public async Task<Guid> GetBookId(string genre)
        {
            return await _bookService.GetBookId(genre);
        }

        [HttpGet, Route("get")]
        public async Task<Guid> GetBookId()
        {
            return await _bookService.GetBookId();
        }

        [HttpGet, Route("sell/{id}")]
        public async Task<Book> SellBook(Guid id)
        {
          return await _bookService.SellBook(id);
        }

        [HttpGet, Route("sell/all")]
        public async Task<List<Book>> SellAll()
        {
            await _bookService.SellAllBook();
            return await GetBooks();
        }
        [HttpGet, Route("discount/{genre}&{discount}")]
        public async Task<List<Book>> MakeDiscount(string genre, decimal discount)
        {
            await _bookService.MakeDiscount(genre, discount);
            return await GetBooks();
        }
    }
}
