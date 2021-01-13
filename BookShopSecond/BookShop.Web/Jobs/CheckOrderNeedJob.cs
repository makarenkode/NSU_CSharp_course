﻿using System.Threading.Tasks;
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
        //#warning по такому названию непонятно что делает Job 
        public CheckOrderNeedJob(BookService bookService, NeedBookProducer needBookProducer)
        {
            _bookService = bookService;
            _needBookProducer = needBookProducer;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //#warning сравнение с true можно опустить
            //#warning исправил
            if (await _bookService.NeedBooksAsync())
            {
              await  _needBookProducer.SentBookReceivedEvent(10);
            }
        }
    }
}
