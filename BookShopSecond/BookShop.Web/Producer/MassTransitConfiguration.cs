using System;

namespace BookShop.Web.Producer
{
    public class MassTransitConfiguration
    {
        public string Address { get; set; }
        public string SharedAccessKeyName { get; set; }
        public string SharedAccessKey { get; set; }


        public Uri GetQueueAddress(string queueName)
        {
            return new Uri(Address + "/" + queueName);
        }
    }
}