using Quartz;

namespace QuartzPoC.Entry.Jobs
{
    public class BasicJob : IJob
    {
        private readonly ILogger<BasicJob> _logger;

        public BasicJob(ILogger<BasicJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            //_logger.LogInformation($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] Basic job");
            return Task.CompletedTask;
        }
    }
}
