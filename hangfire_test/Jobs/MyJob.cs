using Hangfire;

namespace hangfire_test.Jobs
{
    public interface IMyJob
    {
        [Queue("default")]
        [AutomaticRetry(Attempts = 0)]
        Task RunAsync(int id);
    }

    public interface IMyJob2
    {
        [Queue("beta")]
        [AutomaticRetry(Attempts = 0)]
        Task RunAsync(int id);
    }

    public class MyJob : IMyJob
    {
        private readonly ILogger<MyJob> logger;

        public MyJob(ILogger<MyJob> logger)
        {
            this.logger = logger;
        }

        public async Task RunAsync(int id)
        {
            logger.LogInformation($"{Thread.CurrentThread.ManagedThreadId} - {DateTime.Now}");
            await Task.Delay(id);
        }
    }


    public class MyJob2 : IMyJob2
    {
        private readonly ILogger<MyJob2> logger;

        public MyJob2(ILogger<MyJob2> logger)
        {
            this.logger = logger;
        }

        public async Task RunAsync(int id)
        {
            logger.LogInformation($"MyJob2 - {Thread.CurrentThread.ManagedThreadId} - {DateTime.Now}");
            await Task.Delay(id);
        }
    }
}
