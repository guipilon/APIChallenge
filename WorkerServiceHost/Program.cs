using WorkerServiceHost;
using Hangfire;
using Application.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Jobs;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    services.AddHostedService<Worker>();
    services.AddScoped<DataUpsertJob>();
    //services.AddDbContext<DbContext>(options => options.UseSqlServer("Data Source=localhost;Persist Security Info=True;User ID=user;Password=Caneca1012;Encrypt=False"));
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped<DataUpsertJob>();
    //services.AddHttpClient<DataUpsertJob>(client =>
    //{
    //    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
    //}) TODO: implement retry police
    services.AddHttpClient<DataUpsertJob>();
    services.AddHangfire(config => config.UseSqlServerStorage("Data Source=localhost;Persist Security Info=True;User ID=user;Password=Caneca1012;Encrypt=False"));
    services.AddHangfireServer();
});

builder.Build().Run();

//static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
//{
//    return HttpPolicyExtensions
//        .HandleTransientHttpError()
//        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
//        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
//}
