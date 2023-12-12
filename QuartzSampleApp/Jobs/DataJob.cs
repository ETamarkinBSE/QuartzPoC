using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace QuartzSampleApp.Jobs
{
    public class DataJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;
            Console.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {jobData["someKey"]}");
            return Task.CompletedTask;
        }
    }
}
