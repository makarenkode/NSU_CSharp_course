using ContractLibrary.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//#warning неиспользуемые using'и. Проверь во всем коде и убери
//#warning убрал не нужный using
using System.Net.Http;
using System.Threading.Tasks;

namespace BookProvider.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private string _endPoint = "https://getbooksrestapi.azurewebsites.net/api/books/";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<JsonBook>> GetBooks(int count)
        {

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
#warning на мой субъективный взгляд, лучше использовать интерполяцию строк 
#warning $"{_endPoint}{count.ToString()}";
#warning ready
                RequestUri = new Uri($"{_endPoint}{count.ToString()}"),

               
            };
            var response = await _httpClient.SendAsync(httpRequest);
            var json = await response.Content.ReadAsStringAsync();
            var jsonBooks = JsonConvert.DeserializeObject<List<JsonBook>>(json);
            return jsonBooks;
        }
    }
}
