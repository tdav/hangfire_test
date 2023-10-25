using Hangfire;
using System.Diagnostics;

namespace hangfire_test.Jobs
{
    public class HarnessHostedService : BackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly ILogger<HarnessHostedService> _logger;

        public HarnessHostedService(IBackgroundJobClient backgroundJobs, ILogger<HarnessHostedService> logger)
        {
            _backgroundJobs = backgroundJobs ?? throw new ArgumentNullException(nameof(backgroundJobs));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sw = Stopwatch.StartNew();

            var rnd = new Random();

            Parallel.For(0, 25_000, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, i =>
            {

                _backgroundJobs.Enqueue<IMyJob>(x => x.RunAsync(rnd.Next(1, 10)));
                _backgroundJobs.Enqueue<IMyJob2>(x => x.RunAsync(rnd.Next(1, 10)));

            });

            _logger.LogInformation($"Enqueued in {sw.Elapsed}");

            return Task.CompletedTask;
        }
    }
}
