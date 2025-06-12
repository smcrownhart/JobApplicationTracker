using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using JobApplicationTracker.DataAccess.Models;
using Application = JobApplicationTracker.DataAccess.Models.Application;


namespace JobAppTracker.Maui.Services
{
    public class LocalApplicationStorageService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        private string FileName = "applications.json";

        private string GetFilePath()
        {
            return Path.Combine(FileSystem.AppDataDirectory, FileName);
        }

        public async Task<List<Application>> LoadApplicationsAsync()
        {
            try
            {
                var filePath = GetFilePath();
                if (File.Exists(filePath))
                {
                    var jsnon = await File.ReadAllTextAsync(filePath);
                    return JsonSerializer.Deserialize<List<Application>>(jsnon) ?? new List<Application>();
                }

                return new List<Application>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading applications: {ex.Message}");
                return new List<Application>();
            }
        }
        public async Task<Application> GetApplicationByIdAsync(int id)
        {
            var apps = await LoadApplicationsAsync();
            return apps.FirstOrDefault(a => a.Id == id);
        }
        public async Task SaveApplicationsAsync(List<Application> applications)
        {
            try
            {
                var json = JsonSerializer.Serialize(applications, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(GetFilePath(), json);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error saving applications: {ex.Message}");
            }
        }

        public async Task<Application> AddApplicationsAsync(Application app)
        {
            var applications = await LoadApplicationsAsync();

            if (app.Id == 0)
            {
                app.Id = applications.Count > 0 ? applications.Max(a => a.Id) + 1 : 1;
            }

            applications.Add(app);
            await SaveApplicationsAsync(applications);

            return app;
        }

        public async Task UpdateApplicationAsync(Application updatedApp)
        {
            var applications = await LoadApplicationsAsync();
            var index = applications.FindIndex(a => a.Id == updatedApp.Id);

            if (index != -1)
            {
                applications[index] = updatedApp;
                await SaveApplicationsAsync(applications);
            }
            else
            {
                throw new Exception($"Application {updatedApp.Id} not found");
            }

            
        }

        public async Task DeleteApplicationAsync(int id)
        {
            var applications = await LoadApplicationsAsync();
            var deleteApp = applications.FirstOrDefault(a => a.Id == id);

            if (deleteApp != null)
            {
                applications.Remove(deleteApp);
                await SaveApplicationsAsync(applications);
            }
            else
            {
                throw new Exception($"Application {id} not found");
            }
        }
    }
}
