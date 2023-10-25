using AsbtCore.UtilsV2;
using Hangfire;
using Hangfire.MemoryStorage;
using hangfire_test.Jobs;

namespace hangfire_test.Services
{
    public static class HangfireService
    {
        public static void AddMyCronJobs(this IServiceCollection services, IConfiguration co)
        {

            services.AddScoped<IMyJob, MyJob>();
            services.AddScoped<IMyJob2, MyJob2>();

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage(new MemoryStorageOptions());
                config.UseSerilogLogProvider();
            });

            var wc = co["BackgroundJobStatus:WorkerCount"].ToInt();


            services.AddHangfireServer(action =>
            {
                action.ServerName = $"{Environment.MachineName}_default";
                action.Queues = new[] { "default" };
                action.WorkerCount = wc;
            });

            services.AddHangfireServer(action =>
            {
                action.ServerName = $"{Environment.MachineName}_beta";
                action.Queues = new[] { "beta" };
                action.WorkerCount = wc;
            });

        }

        public static void UseMyCronJobs(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
        }
    }
}


