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
#warning теоретически, при такой реализации какая-то одна книга может не сохраниться, и всё сообщение упадёт в очередь ошибок
#warning что не позволит в дальнейшем снова закинуть его для обработки в первоначальном виде
#warning т.к. часть уже будет добавлена. 
#warning можно обернуть вызов AddBook в try/catch, в случае какого-то exception'a логировать, что произошло и какая книга не добавилась
#warning тогда это можно будет по логам найти и досохранить несохранённое
#warning ready, очень крутой совет!
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
