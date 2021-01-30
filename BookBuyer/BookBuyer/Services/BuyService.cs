using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContractLibrary.JsonModels;
using Newtonsoft.Json;

namespace BookBuyer.Services
{
    public class BuyService
    {
        private readonly HttpClient _httpClient;

        private const string EndPoint = "https://bookshopweb.azurewebsites.net/books/";
        private const string SellTag = "sell/";
        private const string GetTag = "get/";

        public BuyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JsonBook>> GetBooks()
        {

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{EndPoint}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<JsonBook>>(json);
            return books;
        }

        public async Task<JsonBook> BuyBook(Guid id)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{EndPoint}{SellTag}{id.ToString()}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<JsonBook>(json);
            return book;
        }

        public async Task<Guid> GetBookId(string genre)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{EndPoint}{GetTag}{genre}"),
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
                RequestUri = new Uri($"{EndPoint}{GetTag}"),
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<Guid>(json);
            return id;
        }
    }
}
