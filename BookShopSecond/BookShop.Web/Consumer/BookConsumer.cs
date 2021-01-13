using System;
using System.Threading.Tasks;
using BookShop.Web.Services;
using ContractLibrary;
using MassTransit;

namespace BookShop.Web.Consumer
{
    public class BookConsumer : IConsumer<IBookListContract>
    {
        private readonly BookService _bookService;

        public BookConsumer(BookService bookService)
        {
            _bookService = bookService;
        }
        public async Task Consume(ConsumeContext<IBookListContract> context)
        {
            var message = context.Message;
            foreach(var book in message.JBooks)
            {
                await _bookService.AddBookAsync(_bookService.JsonToBook(book));
            }

            Console.WriteLine($" Books: {message.JBooks}");
            
        }
    }
}
