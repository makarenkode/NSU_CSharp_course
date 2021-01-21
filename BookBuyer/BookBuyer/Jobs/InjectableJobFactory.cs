using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace BookBuyer.Jobs
{
    public sealed class InjectableJobFactory : SimpleJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public InjectableJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob)_serviceProvider.GetRequiredService(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                throw new SchedulerException($"Problem while instantiating job '{bundle.JobDetail.Key}' from the injectJobFactory.", e);
            }
        }
    }
}
