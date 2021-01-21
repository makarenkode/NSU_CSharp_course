using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookBuyer.Services
{
    public class BuyService
    {
        private readonly HttpClient _httpClient;

        private string _endPoint = "https://bookshopweb.azurewebsites.net/books/";
        private string _sellTag = "sell/";
        private string _getTag = "get/";

        public BuyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetBooks()
        {

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_endPoint}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(json);
            return books;
        }

        public async Task<Book> BuyBook(Guid id)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_endPoint}{_sellTag}{id.ToString()}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(json);
            return book;
        }

        public async Task<Guid> GetBookId(string genre)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_endPoint}{_getTag}{genre}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<Guid>(json);
            return id;
        }
        public async Task<Guid> GetBookId()
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_endPoint}{_getTag}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<Guid>(json);
            return id;
        }
    }
}
