using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace QuartzSampleApp
{
    public class JobListener : IJobListener
    {
        public string Name => "JobListener";
        public string MyName { get; set; }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Job Executed Listener");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"{context.JobDetail.Key} fired at {context.FireTimeUtc.AddHours(2)}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"{context.JobDetail.Key} finished firing at {context.FireTimeUtc.AddHours(2)}");
            return Task.CompletedTask;
        }
    }
}
