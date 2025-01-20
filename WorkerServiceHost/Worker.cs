using Hangfire;

namespace WorkerServiceHost
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            RecurringJob.AddOrUpdate("DataUpsertJob", () => ExecuteJob(), Cron.Hourly);
            return Task.CompletedTask;
        }

        public Task ExecuteJob()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var job = scope.ServiceProvider.GetRequiredService<Jobs.DataUpsertJob>();
                return job.ExecuteAsync();
            }
        }
    }
}