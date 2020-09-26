using Hangfire;
using HangfireDemo.Server.Jobs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HangfireDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly IRecurringJobManager _recurringJobManager;

        public JobsController(IBackgroundJobClient backgroundJobs, IRecurringJobManager recurringJobManager)
        {
            _backgroundJobs = backgroundJobs;
            _recurringJobManager = recurringJobManager;
        }

        [HttpPost]
        [Route("hello")]
        public IActionResult HelloWorld()
        {
            _backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            return Ok();
        }

        [HttpPost]
        [Route("long")]
        public IActionResult LongJob()
        {
            _backgroundJobs.Enqueue<LongJob>(x => x.DoWork());

            return Ok();
        }

        [HttpPost]
        [Route("cron")]
        public IActionResult Cron()
        {
            _recurringJobManager.AddOrUpdate("CronDemo", () => Console.WriteLine($"Repeat!"), "0 * * ? * *");

            return Ok();
        }

        [HttpPost]
        [Route("cron/remove")]
        public IActionResult CronRemove()
        {
            _recurringJobManager.RemoveIfExists("CronDemo");

            return Ok();
        }
    }
}