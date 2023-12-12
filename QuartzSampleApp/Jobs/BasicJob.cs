using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace QuartzSampleApp.Jobs
{
    public class BasicJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Basic job");
            return Task.CompletedTask;
        }
    }
}
