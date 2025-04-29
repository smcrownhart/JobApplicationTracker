using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using JobApplicationTracker.DataAccess.Models;
using Application = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.Services
{
    public class ApplicationApiService
    {
        private readonly HttpClient _httpClient;

        public ApplicationApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("JobAppTrackerApiService");
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Application>>("application");
            return response ?? new List<Application>();
        }

        public async Task<Application> CreateApplicationAsync(Application app)
        {
            var response = await _httpClient.PostAsJsonAsync("application", app);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Application>();
            }
            else
            {
                throw new Exception("Failed to create application");
            }
        }
    }
}
