using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using QuartzPoC2.Entry.Jobs;

namespace QuartzPoC2.Entry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulerController : ControllerBase
    {
        private readonly ISchedulerFactory _factory;

        private readonly ILogger<SchedulerController> _logger;

        public SchedulerController(ILogger<SchedulerController> logger, ISchedulerFactory factory)
        {
            _logger = logger;
            _logger.LogInformation("SchedulerController Constructor");
            _factory = factory;
        }

        [HttpPost(Name = "ScheduleJob")]
        public async Task<ActionResult<string>> ScheduleJob()
        {
            var scheduler = await  _factory.GetScheduler();
            var job1 = JobBuilder.Create<SimpleJob>()
                .WithIdentity("job2", "group1")
                .Build();
            var trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();
            await scheduler.ScheduleJob(job1, trigger1);
            return new ActionResult<string>($"{job1.Key} created!");
        }
    }
}