// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Specialized;
using System.Reflection;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using QuartzSampleApp;
using QuartzSampleApp.Jobs;
using QuartzSampleApp.Loggers;

// set up logger
LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
// create scheduler
var schedulerFactory = new StdSchedulerFactory();
schedulerFactory.Initialize();
var scheduler = await schedulerFactory.GetScheduler();
//var listener = new JobListener();
//scheduler.ListenerManager.AddJobListener(listener);
// jobs
// regular job
var job1 = JobBuilder.Create<BasicJob>()
    .WithIdentity("job1", "group1")
    .Build();
// delayed job
var job2 = JobBuilder.Create<DelayedJob>()
    .WithIdentity("job2", "group1")
    .Build();
// job with timezone
var job3 = JobBuilder.Create<BasicJob>()
    .WithIdentity("job3", "group3")
    .Build();
// job with cron schedule
var job4 = JobBuilder.Create<HelloJob>()
    .WithIdentity("job4", "group1")
    .Build();
// job with data and re-fire
var job5 = JobBuilder.Create<RecoverableJob>()
    .WithIdentity("job5", "group1")
    .SetJobData(new JobDataMap
    {
        {"someKey", "someValue"}
    })
    .RequestRecovery()
    .StoreDurably()
    .Build();
// triggers
var trigger1 = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x 
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();
var trigger2 = TriggerBuilder.Create()
    .WithIdentity("trigger2", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(3)
        .RepeatForever())
    .Build();
var romanZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
var romanceTimeOffset = new DateTimeOffset(
        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour - 1, DateTime.Now.Minute,
            DateTime.Now.Second), romanZoneInfo.BaseUtcOffset);
var trigger3 = TriggerBuilder.Create()
    .WithIdentity("trigger3", "group1")
    .StartAt(romanceTimeOffset)
    .WithSimpleSchedule(x => x
        .WithMisfireHandlingInstructionNextWithRemainingCount()
    )
    .Build();
var cronExpression = "10 55,56 9 ? * * *";
var cronSchedule = CronScheduleBuilder.CronSchedule(new CronExpression(cronExpression));
var trigger4 = TriggerBuilder.Create()
    .WithIdentity("trigger4", "group1")
    .WithSchedule(cronSchedule)
    .StartNow()
    .Build();
var trigger5 = TriggerBuilder.Create()
    .WithIdentity("trigger5", "group1")
    .StartAt(DateTime.Now)
    .ForJob(job5)
    .Build();
// job scheduling
await scheduler.ScheduleJob(job1, trigger1);
//await scheduler.ScheduleJob(job2, trigger2);
//await scheduler.ScheduleJob(job3, trigger3);
//await scheduler.ScheduleJob(job4, trigger4);
//await scheduler.ScheduleJob(job5, trigger5);
await scheduler.Start();
//await scheduler.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals("group1"));
await Task.Delay(TimeSpan.FromSeconds(200));
await scheduler.Shutdown();
