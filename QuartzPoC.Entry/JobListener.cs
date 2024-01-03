using Quartz;
using QuartzPoC.Entry.Jobs;

namespace QuartzPoC.Entry
{
    public class JobListener : IJobListener
    {
        public string Name => "JobListener";
        private readonly IQuartzSettings _quartzSettings;
        private readonly ILogger<JobListener> _logger;

        public JobListener(ILogger<JobListener> logger, IQuartzSettings quartzSettings)
        {
            this._logger = logger;
            this._quartzSettings = quartzSettings;
        }
        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Job Executed Listener");
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"{context.JobDetail.Key} fired at {context.FireTimeUtc.AddHours(2)}");
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            //var monitoringFilePath = _quartzSettings.MonitoringFilePath;
            //if (!File.Exists(monitoringFilePath))
            //{
            //    File.Create(monitoringFilePath);
            //}

            //var executionStatus = jobException == null ? "Success" : "Fail";
            //var executionInfo =
            //    $"{context.JobDetail.Key} finished firing at {context.FireTimeUtc.ToLocalTime():MM/dd/yyyy HH:mm:ss} with status: {executionStatus}\n";
            //File.AppendAllText(monitoringFilePath, executionInfo);
            if (!context.JobDetail.Key.Equals(JobKey.Create("job1", "group2")))
            {
                var result = "";
                result += $"{context.JobDetail.Key};";
                result += $"{context.FireTimeUtc.ToLocalTime():MM/dd/yyyy HH:mm:ss};";
                var executionStatus = jobException == null ? "Success" : "Fail";
                result += $"{executionStatus};";
                result += $"{context.NextFireTimeUtc?.ToLocalTime():MM/dd/yyyy HH:mm:ss};";
                ManagerJob.JobDetails.Add(result);
            }
            return Task.CompletedTask;
        }
    }
}
