using System.Threading.Tasks;
using BookProvider.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookProvider.Producer
{
    public class BookProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;
        private readonly ApiService _apiService;

        public BookProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration, ApiService apiService)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
            _apiService = apiService;
        }

        public async Task SentBookReceivedEvent(int BookQuantity)
        {
            var message = new BookContract
            {
                JBooks = await _apiService.GetBooks(BookQuantity)
            };

            var hostConfig = new MassTransitConfiguration();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(hostConfig.GetQueueAddress("Receive-queue"));
            await endpoint.Send(message);
        }

    }
}
