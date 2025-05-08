using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.Maui.Services
{
    public class LocalCheckedOnAppStorageService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private const string FileName = "checkedonapps.json";

        private string GetFilePath() => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task<List<CheckedOnApp>> LoadCheckedOnAppsAsync()
        {
            if (!File.Exists(GetFilePath())) return new();
            var json = await File.ReadAllTextAsync(GetFilePath());
            return JsonSerializer.Deserialize<List<CheckedOnApp>>(json, _jsonOptions) ?? new();
        }

        public async Task SaveCheckedOnAppsAsync(List<CheckedOnApp> entries)
        {
            var json = JsonSerializer.Serialize(entries, _jsonOptions);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        public async Task AddCheckedOnAppAsync(CheckedOnApp item)
        {
            var entries = await LoadCheckedOnAppsAsync();
            item.Id = entries.Any() ? entries.Max(e => e.Id) + 1 : 1;
            entries.Add(item);
            await SaveCheckedOnAppsAsync(entries);
        }

        public async Task UpdateCheckedOnAppAsync(CheckedOnApp updated)
        {
            var entries = await LoadCheckedOnAppsAsync();
            var index = entries.FindIndex(e => e.Id == updated.Id);
            if (index >= 0)
            {
                entries[index] = updated;
                await SaveCheckedOnAppsAsync(entries);
            }
        }

        public async Task DeleteCheckedOnAppAsync(int id)
        {
            var entries = await LoadCheckedOnAppsAsync();
            var target = entries.FirstOrDefault(e => e.Id == id);
            if (target != null)
            {
                entries.Remove(target);
                await SaveCheckedOnAppsAsync(entries);
            }
        }
    }
}
