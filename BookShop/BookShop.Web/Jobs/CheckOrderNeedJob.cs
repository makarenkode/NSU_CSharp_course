using System.Threading.Tasks;
using BookShop.Web.Producer;
using BookShop.Web.Services;
using JetBrains.Annotations;
using Quartz;

namespace BookShop.Web.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class CheckOrderNeedJob : IJob
    {
        private readonly BookService _bookService;
        private readonly NeedBookProducer _needBookProducer;
        public CheckOrderNeedJob(BookService bookService, NeedBookProducer needBookProducer)
        {
            _bookService = bookService;
            _needBookProducer = needBookProducer;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            if (await _bookService.NeedBooks())
            {
              await  _needBookProducer.SentBookReceivedEvent(10);
            }
        }
    }
}
