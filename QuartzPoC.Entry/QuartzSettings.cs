namespace QuartzPoC.Entry
{
    public class QuartzSettings : IQuartzSettings
    {
        public string TopicName { get; set; } = "";
        public string CronSchedule { get; set; } = "";
        public string MonitoringFilePath { get; set; } = "";
    }
}
