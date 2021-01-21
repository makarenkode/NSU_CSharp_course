using System.Threading.Tasks;
using BookBuyer.Services;
using JetBrains.Annotations;
using Quartz;

namespace BookBuyer.Jobs
{
    [UsedImplicitly]
    [DisallowConcurrentExecution]
    public class BuyBookJob : IJob
    {
        private readonly BuyService _buyService;
        public BuyBookJob(BuyService buyService)
        {
            _buyService = buyService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var id = await _buyService.GetBookId();
            await _buyService.BuyBook(id);
        }
    }
}
