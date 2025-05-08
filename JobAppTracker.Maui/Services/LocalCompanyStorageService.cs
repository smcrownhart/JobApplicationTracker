using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.Maui.Services
{
    public class LocalCompanyStorageService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private const string FileName = "companies.json";

        private string GetFilePath() => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task<List<Company>> LoadCompaniesAsync()
        {
            if (!File.Exists(GetFilePath())) return new();
            var json = await File.ReadAllTextAsync(GetFilePath());
            return JsonSerializer.Deserialize<List<Company>>(json, _jsonOptions) ?? new();
        }

        public async Task SaveCompaniesAsync(List<Company> companies)
        {
            var json = JsonSerializer.Serialize(companies, _jsonOptions);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        public async Task AddCompanyAsync(Company company)
        {
            var companies = await LoadCompaniesAsync();
            company.Id = companies.Any() ? companies.Max(c => c.Id) + 1 : 1;
            companies.Add(company);
            await SaveCompaniesAsync(companies);
        }

        public async Task UpdateCompanyAsync(Company updated)
        {
            var companies = await LoadCompaniesAsync();
            var index = companies.FindIndex(c => c.Id == updated.Id);
            if (index >= 0)
            {
                companies[index] = updated;
                await SaveCompaniesAsync(companies);
            }
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var companies = await LoadCompaniesAsync();
            var toDelete = companies.FirstOrDefault(c => c.Id == id);
            if (toDelete != null)
            {
                companies.Remove(toDelete);
                await SaveCompaniesAsync(companies);
            }
        }
    }
}
