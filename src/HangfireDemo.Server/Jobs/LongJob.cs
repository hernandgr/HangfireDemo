using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace HangfireDemo.Server.Jobs
{
    public class LongJob
    {
        private readonly ILogger<LongJob> _logger;

        public LongJob(ILogger<LongJob> logger)
        {
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.LogInformation("Starting long job");

            Thread.Sleep(TimeSpan.FromSeconds(20));

            _logger.LogInformation("Finished long job");
        }
    }
}