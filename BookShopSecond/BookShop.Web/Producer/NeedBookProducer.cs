
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookShop.Web.Producer
{
    public class NeedBookProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public NeedBookProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task SentBookReceivedEvent(int bookQuantity)
        {
            var message = new NeedBookContract
            {
                BookQuantity = bookQuantity,
            };

            var hostConfig = new MassTransitConfiguration();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(hostConfig.GetQueueAddress("Request-queue"));
            await endpoint.Send(message);
        }

    }
}