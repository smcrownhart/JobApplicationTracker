using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.Maui.Services
{
    public class LocalCompanyContactStorageService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private const string FileName = "companycontacts.json";

        private string GetFilePath() =>
            Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task<List<CompanyContact>> LoadContactsAsync()
        {
            var path = GetFilePath();
            if (!File.Exists(path)) return new List<CompanyContact>();

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<CompanyContact>>(json, _jsonOptions)
                ?? new List<CompanyContact>();
        }

        public async Task SaveContactsAsync(List<CompanyContact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts, _jsonOptions);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        public async Task AddContactAsync(CompanyContact contact)
        {
            var contacts = await LoadContactsAsync();
            contact.Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            await SaveContactsAsync(contacts);
        }

        public async Task UpdateContactAsync(CompanyContact updated)
        {
            var contacts = await LoadContactsAsync();
            var index = contacts.FindIndex(c => c.Id == updated.Id);
            if (index >= 0)
            {
                contacts[index] = updated;
                await SaveContactsAsync(contacts);
            }
        }

        public async Task DeleteContactAsync(int id)
        {
            var contacts = await LoadContactsAsync();
            var toDelete = contacts.FirstOrDefault(c => c.Id == id);
            if (toDelete != null)
            {
                contacts.Remove(toDelete);
                await SaveContactsAsync(contacts);
            }
        }
    }
}
