using FluentScheduler;
using MantiScanServices.BackgroundJobs;
using MantiScanServices.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MantiScanServices.BackgroundServices
{
    public class SchedulingService : BaseBackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly AnraConfiguration configuration;
        private readonly ILogger<string> logger;

        public SchedulingService(ILogger<string> _logger, IServiceProvider serviceProvider, AnraConfiguration configuration)
        {
            logger = _logger;
            this.serviceProvider = serviceProvider;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            #if DEBUG
                logger.LogInformation("ANRA::: BackgroundJobSchedulingService");
            #endif

            JobManager.AddJob(() => new NotifyMailJob(serviceProvider, logger).Execute(), (s) => s.WithName("NotifyMailJob").ToRunEvery(1).Days());

            await Task.Delay(10, stoppingToken);
        }

        public override void Dispose()
        {
            JobManager.StopAndBlock();
            _stoppingCts.Cancel();
        }
    }
}
