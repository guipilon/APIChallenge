using Application.Interfaces;
using Domain;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs
{
    public class DataUpsertJob
    {
        private readonly IServiceProvider _serviceProvider;

        public DataUpsertJob(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<Job>>();
                var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();

                var dataSourceUrl = "https://jobicy.com/api/v2/remote-jobs?count=50&geo=usa"; 
                var response = await httpClient.GetAsync(dataSourceUrl);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

                var data = apiResponse?.Jobs;

                if (data != null && data.Any())
                {
                    foreach (var item in data)
                    {
                        var itemAlreadyExists = await repository.ExistsByJobIdAsync(item.JobId);
                        if (!itemAlreadyExists)
                            await repository.AddAsync(item);
                    }
                }
            }
        }
    }
}