using Quartz;

namespace QuartzPoC2.Entry.Jobs
{
    public class SimpleJob : IJob
    {
        private readonly ILogger<SimpleJob> _logger;

        public SimpleJob(ILogger<SimpleJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Simple job");
            return Task.CompletedTask;
        }
    }
}
