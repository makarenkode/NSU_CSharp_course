using ContractLibrary.JsonModels;
using BookProvider.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookProvider.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController
    {
        private readonly ApiService _apiService;

        public BookController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet, Route("{count}")]
        public async Task<List<JsonBook>> GetBooks(int count)
        {
            return await _apiService.GetBooks(count);

        }
    }
}
