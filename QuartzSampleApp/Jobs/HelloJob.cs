using Quartz;
using System;

namespace QuartzSampleApp.Jobs
{
    public class HelloJob : IJob
    {
        private int _repNum = 1;
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Started the task");
            await Task.Delay(TimeSpan.FromSeconds(15));
            await Console.Out.WriteLineAsync($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Greetings from HelloJob");
            _repNum++;
        }
    }
}
