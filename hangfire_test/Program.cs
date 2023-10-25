using hangfire_test.Jobs;
using hangfire_test.Services;
using Serilog;

namespace hangfire_test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, logConfig) => logConfig
                        .ReadFrom.Configuration(context.Configuration));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            builder.Services.AddMyCronJobs(builder.Configuration);
            builder.Services.AddHostedService<HarnessHostedService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMyCronJobs();

            app.MapControllers();

            app.UseSerilogRequestLogging();

            app.Run();
        }
    }
}