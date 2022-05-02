using Quartz;
using Quartz.Spi;
using System.Collections.Concurrent;

namespace CDL.Integration.Workers.Factories
{
    public class ScheduledJobFactory : IJobFactory, IDisposable
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly ConcurrentDictionary<IJob, IServiceScope> escopos = new ConcurrentDictionary<IJob, IServiceScope>();

        public ScheduledJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IServiceScope escopo = serviceProvider.CreateScope();
            IJob job = escopo.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

            this.escopos.TryAdd(job, escopo);
            return job;
        }

        public void ReturnJob(IJob job)
        {
            try
            {
                (job as IDisposable)?.Dispose();

                if (this.escopos.TryRemove(job, out IServiceScope escopo))
                    escopo.Dispose();
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            try
            {
                foreach (var escopo in this.escopos.Values)
                {
                    escopo?.Dispose();
                }
            }
            catch (Exception) { }
        }
    }
}
