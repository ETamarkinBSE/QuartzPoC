using Microsoft.VisualBasic;
using Quartz;

namespace QuartzPoC2.Entry.Jobs
{
    public class ManagerJob : IJob
    {
        public static List<string> JobDetails { get; set; } = new ();
        private readonly ILogger<ManagerJob> _logger;

        public ManagerJob(ILogger<ManagerJob> logger)
        {
            this._logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var result = "";
            if (JobDetails.Count == 0)
            {
                return Task.CompletedTask;
            }
            var indexedJobDetails = JobDetails.Select((value, index) => new { Value = value, Index = index });
            foreach (var detail in indexedJobDetails)
            {
                if (detail.Index > 0)
                {
                    result += "".PadLeft(6);
                }
                var splitDetails = detail.Value.Split(new[] { ';' });
                foreach (var splitDetail in splitDetails)
                {
                    var padRightAmount = splitDetail.Length > 15 ? 30 : 20;
                    result += splitDetail.PadRight(padRightAmount);
                }
                result += "\n";
            }
            _logger.LogInformation("{result}",result);
            return Task.CompletedTask;
        }
    }
}
