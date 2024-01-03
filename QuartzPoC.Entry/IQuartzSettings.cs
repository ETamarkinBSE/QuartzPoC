namespace QuartzPoC.Entry
{
    public interface IQuartzSettings
    { 
        string TopicName { get; set; }
        string CronSchedule { get; set; }
        string MonitoringFilePath { get; set; }
    }
}
