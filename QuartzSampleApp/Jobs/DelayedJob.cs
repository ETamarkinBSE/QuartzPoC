using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace QuartzSampleApp.Jobs
{
    public class DelayedJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Started delayed job with 5 seconds delay!");
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] finished delayed job");
        }
    }
}
