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
    public class LocalInterviewPrepStorageService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private const string FileName = "interviewprep.json";

        private string GetFilePath() => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task<List<InterviewPrep>> LoadPrepAsync()
        {
            if (!File.Exists(GetFilePath())) return new();
            var json = await File.ReadAllTextAsync(GetFilePath());
            return JsonSerializer.Deserialize<List<InterviewPrep>>(json, _jsonOptions) ?? new();
        }

        public async Task SavePrepAsync(List<InterviewPrep> prep)
        {
            var json = JsonSerializer.Serialize(prep, _jsonOptions);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        public async Task AddPrepAsync(InterviewPrep item)
        {
            var prep = await LoadPrepAsync();
            item.Id = prep.Any() ? prep.Max(p => p.Id) + 1 : 1;
            prep.Add(item);
            await SavePrepAsync(prep);
        }

        public async Task UpdatePrepAsync(InterviewPrep updated)
        {
            var prep = await LoadPrepAsync();
            var index = prep.FindIndex(p => p.Id == updated.Id);
            if (index >= 0)
            {
                prep[index] = updated;
                await SavePrepAsync(prep);
            }
        }

        public async Task DeletePrepAsync(int id)
        {
            var prep = await LoadPrepAsync();
            var item = prep.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                prep.Remove(item);
                await SavePrepAsync(prep);
            }
        }
    }
}
