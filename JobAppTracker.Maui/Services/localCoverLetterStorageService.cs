using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.Maui.Services
{
    public class localCoverLetterStorageService
    {
        private readonly string _filePath;

        public localCoverLetterStorageService()
        {
            var folder = FileSystem.AppDataDirectory;
            _filePath = Path.Combine(folder, "coverLetters.json");
        }

        public async Task<List<CoverLetter>> GetCoverLettersAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<CoverLetter>();
            }
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<CoverLetter>>(json) ?? new List<CoverLetter>();
        }

        public async Task SaveCoverLettersAsync(List<CoverLetter> coverLetters)
        {
            var json = JsonSerializer.Serialize(coverLetters);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task AddCoverLetterAsync(CoverLetter coverLetter)
        {
            var coverLetters = await GetCoverLettersAsync();
            if (coverLetter.Id == 0)
            {
                coverLetter.Id = coverLetters.Count > 0 ? coverLetters.Max(cl => cl.Id) + 1 : 1;
            }
            coverLetters.Add(coverLetter);
            await SaveCoverLettersAsync(coverLetters);
        }

        public async Task UpdateCoverLetterAsync(CoverLetter coverLetter)
        {
            var coverLetters = await GetCoverLettersAsync();
            var existingCoverLetter = coverLetters.FirstOrDefault(cl => cl.Id == coverLetter.Id);
            if (existingCoverLetter != null)
            {
                existingCoverLetter.letterContent = coverLetter.letterContent;
                await SaveCoverLettersAsync(coverLetters);
            }
        }
        public async Task DeleteAllCoverLettersAsync()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath); // 💣 Boom. Nuke it.
            }
        }

        public async Task DeleteCoverLetterAsync(int id)
        {
            var coverLetters = await GetCoverLettersAsync();
            var coverLetterToDelete = coverLetters.FirstOrDefault(cl => cl.Id == id);
            if (coverLetterToDelete != null)
            {
                coverLetters.Remove(coverLetterToDelete);
                await SaveCoverLettersAsync(coverLetters);
            }
        }

    }
}
