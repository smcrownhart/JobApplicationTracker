using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobAppTracker.Maui.Services
{
    public class LocalInterviewStorageService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private const string FileName = "interviews.json";

        private string GetFilePath() => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task<List<Interviews>> LoadInterviewsAsync()
        {
            if (!File.Exists(GetFilePath())) return new();
            var json = await File.ReadAllTextAsync(GetFilePath());
            return JsonSerializer.Deserialize<List<Interviews>>(json, _jsonOptions) ?? new();
        }

        public async Task SaveInterviewsAsync(List<Interviews> interviews)
        {
            var json = JsonSerializer.Serialize(interviews, _jsonOptions);
            await File.WriteAllTextAsync(GetFilePath(), json);
        }

        public async Task AddInterviewAsync(Interviews interview)
        {
            var interviews = await LoadInterviewsAsync();
            interview.Id = interviews.Any() ? interviews.Max(i => i.Id) + 1 : 1;
            interviews.Add(interview);
            await SaveInterviewsAsync(interviews);
        }

        public async Task UpdateInterviewAsync(Interviews updated)
        {
            var interviews = await LoadInterviewsAsync();
            var index = interviews.FindIndex(i => i.Id == updated.Id);
            if (index >= 0)
            {
                interviews[index] = updated;
                await SaveInterviewsAsync(interviews);
            }
        }

        public async Task DeleteInterviewAsync(int id)
        {
            var interviews = await LoadInterviewsAsync();
            var target = interviews.FirstOrDefault(i => i.Id == id);
            if (target != null)
            {
                interviews.Remove(target);
                await SaveInterviewsAsync(interviews);
            }
        }
    }
}
