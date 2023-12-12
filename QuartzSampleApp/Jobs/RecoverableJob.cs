using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzSampleApp.Jobs
{
    public class RecoverableJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dataMap = context.JobDetail.JobDataMap;
                Console.WriteLine($"Recoverable Job is executing with {dataMap["someKey"]}...");

                // Simulate a task that takes some time to complete
                await Task.Delay(TimeSpan.FromSeconds(5));

                // Simulate an issue or crash during the job execution
                throw new Exception("Job failure");
            }
            catch (Exception ex)
            {
                throw new JobExecutionException("Job failed!", ex, true);
            }
        }

    }
}
