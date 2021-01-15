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
                try
                {
                    await _bookService.AddBook(_bookService.JsonToBook(book));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }

            Console.WriteLine($" Books: {message.JBooks}");
            
        }
    }
}
