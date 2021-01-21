using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

namespace BookBuyer.Jobs
{
    [UsedImplicitly]
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private static IScheduler Scheduler { get; set; }

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;
            await ConfigureSimpleJob();
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // ReSharper disable once PossibleNullReferenceException
            await Scheduler?.Shutdown(cancellationToken);
            Scheduler = null;
        }

        private async Task ConfigureSimpleJob()
        {
            var trigger = TriggerBuilder.Create()
                .WithIdentity(nameof(BuyBookJob))
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(300).RepeatForever())
                .Build();
            var job = JobBuilder.Create<BuyBookJob>().WithIdentity(nameof(BuyBookJob)).Build();
            await Scheduler.ScheduleJob(job, trigger);
        }
    }
}
