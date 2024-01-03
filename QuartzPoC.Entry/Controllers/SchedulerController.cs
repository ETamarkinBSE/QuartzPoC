using Microsoft.AspNetCore.Mvc;
using Quartz;
using QuartzPoC.Entry.Jobs;

namespace QuartzPoC.Entry.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchedulerController : ControllerBase
    {
        private readonly ILogger<SchedulerController> _logger;
        private readonly ISchedulerFactory _factory;
        private readonly IQuartzSettings _settings;


        public SchedulerController(ILogger<SchedulerController> logger, ISchedulerFactory factory, IQuartzSettings settings)
        {
            _logger = logger;
            _factory = factory;
            _settings = settings;
        }

        [HttpPost]
        public async Task<ActionResult<string>> ScheduleJob()
        {
            try
            {
                var scheduler = await _factory.GetScheduler();
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
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> PauseJob([FromBody] JobKeyDto keyDto)
        {
            try
            {
                var jobKey = new JobKey(keyDto.Name, keyDto.Group);
                var scheduler = await _factory.GetScheduler();
                await scheduler.PauseJob(jobKey);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> ResumeJob(JobKeyDto keyDto)
        {
            try
            {
                var jobKey = new JobKey(keyDto.Name, keyDto.Group);
                var scheduler = await _factory.GetScheduler();
                await scheduler.ResumeJob(jobKey);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteJob([FromBody] JobKeyDto keyDto)
        {
            try
            {
                var jobKey = new JobKey(keyDto.Name, keyDto.Group);
                var scheduler = await _factory.GetScheduler();
                return await scheduler.DeleteJob(jobKey);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}