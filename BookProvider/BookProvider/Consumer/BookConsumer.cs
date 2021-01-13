﻿using System;
using System.Threading.Tasks;
using ContractLibrary;
using ExternalApi.Producer;
using MassTransit;

namespace ExternalApi.Consumer
{
    public class BookConsumer : IConsumer<IBookContract>
    {
        private readonly BookProducer _bookProducer;
        public BookConsumer(BookProducer bookProducer)
        {
            _bookProducer = bookProducer;
        }
        public async Task Consume(ConsumeContext<IBookContract> context)
        {
            var message = context.Message;
            await _bookProducer.SentBookReceivedEvent(message.BookQuantity);
            Console.WriteLine($"Payment from {message.BookQuantity} ");
            return;
        }
    }
}
