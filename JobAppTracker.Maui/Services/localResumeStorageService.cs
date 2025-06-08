using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.Maui.Services
{
    public class localResumeStorageService
    {
        private readonly string _filePath;

        public localResumeStorageService()
        {
            var folder = FileSystem.AppDataDirectory;
            _filePath = Path.Combine(folder, "resumes.json");

        }

        public async Task<List<Resume>> GetResumeAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Resume>();
            }
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Resume>>(json) ?? new List<Resume>();
        }

        public async Task SaveResumeAsync(List<Resume> resumes)
        {
            var json = JsonSerializer.Serialize(resumes);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task AddResumeAsync(Resume resume)
        {
            var resumes = await GetResumeAsync();
            if (resume.Id == 0)
            {
                resume.Id = resumes.Count > 0 ? resumes.Max(r => r.Id) + 1 : 1;
            }
            resumes.Add(resume);
            await SaveResumeAsync(resumes);
        }

        public async Task UpdateResumeAsync(Resume resume)
        {
            var resumes = await GetResumeAsync();
            var existingResume = resumes.FirstOrDefault(r => r.Id == resume.Id);
            if (existingResume != null)
            {
                existingResume.resumeContent = resume.resumeContent;
                await SaveResumeAsync(resumes);
            }
        }

        public async Task DeleteAllResumesAsync()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath); // 💣 Boom. Nuke it.
            }
        }

        public async Task DeleteResumeAsync(int id)
        {
            var resumes = await GetResumeAsync();
            var resumeToDelete = resumes.FirstOrDefault(r => r.Id == id);
            if (resumeToDelete != null)
            {
                resumes.Remove(resumeToDelete);
                await SaveResumeAsync(resumes);
            }
        }
    }
}
