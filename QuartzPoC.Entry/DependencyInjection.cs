using Quartz;
using Quartz.Impl;
using QuartzPoC2.Entry.Jobs;

namespace QuartzPoC2.Entry
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IQuartzSettings settings)
        {
            services.AddQuartz(options =>
            {
                options.SchedulerName = "MyScheduler";
                var basicJobKey = JobKey.Create("job1", "group1");
                options.AddJob<BasicJob>(basicJobKey);
                options.AddTrigger(trigger =>
                    trigger
                        .ForJob(basicJobKey)
                        .WithCronSchedule(settings.CronSchedule));
                var managerJobKey = JobKey.Create("job1", "group2");
                options.AddJob<ManagerJob>(managerJobKey);
                options.AddTrigger(trigger =>
                    trigger.ForJob(managerJobKey)
                        .WithSimpleSchedule(schedule => schedule
                            .WithIntervalInSeconds(10)
                            .RepeatForever()));
                options.AddJobListener<JobListener>();
            });
            services.AddQuartzHostedService();
        }
    }
}
