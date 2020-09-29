using Hangfire;
using Microsoft.Extensions.Logging;
using System;

namespace HangfireDemo.Server.Jobs
{
    [AutomaticRetry(Attempts = 2)]
    public class ExceptionJob
    {
        private readonly ILogger<ExceptionJob> _logger;

        public ExceptionJob(ILogger<ExceptionJob> logger)
        {
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.LogInformation("Something bad will happen...");

            throw new Exception("Fix me!");
        }
    }
}