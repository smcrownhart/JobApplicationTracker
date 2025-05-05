using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;
using System.Text.Json.Serialization;

namespace JobAppTracker.Maui.Services
{
    public class LocalCompanyStorageService
    {
        private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "companies.json");
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        public async Task<List<Company>> LoadCompaniesAsync()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = await File.ReadAllTextAsync(_filePath);
                    return JsonSerializer.Deserialize<List<Company>>(json, _options) ?? new List<Company>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading companies: {ex.Message}");
            }
            return new List<Company>();
        }

        public async Task AddCompanyAsync(Company company)
        {
            var companies = await LoadCompaniesAsync();
            company.Id = companies.Count > 0 ? companies.Max(c => c.Id) + 1 : 1;
            companies.Add(company);
            var json = JsonSerializer.Serialize(companies);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
