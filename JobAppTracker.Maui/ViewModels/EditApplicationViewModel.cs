using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Models;


namespace JobAppTracker.Maui.ViewModels
{
    
    public class EditApplicationViewModel : INotifyPropertyChanged
    {
      private readonly INavigationHelper _navigationHelper;
        private readonly LocalApplicationStorageService _appService;
        private readonly localResumeStorageService _resumeService;
        private readonly localCoverLetterStorageService _coverLetterService;
        public EditApplicationViewModel(LocalApplicationStorageService appService, localResumeStorageService resumeService,
            localCoverLetterStorageService coverLetterStorageService, INavigationHelper navigationHelper)
        {
            _appService = appService;
            _resumeService = resumeService;
            _coverLetterService = coverLetterStorageService;
            SaveCommand = new Command(async () => await SaveAsync());
            _navigationHelper = navigationHelper;
        }

        public ICommand SaveCommand { get; }

        private EditApplicationDTO _application;

        public EditApplicationDTO Application
        {
            get => _application;
            set
            {
                _application = value;
                OnPropertyChanged();
                LoadResumeAndCoverLetterAsync();
            }
        }
        public List<string> StatusOptions { get; } = new List<string>()
        {
            "Applied",
            "Interviewed",
            "Rejected"
        };

        private string _resumeText;
        public string ResumeText
        {
            get => _resumeText;
            set
            {
                _resumeText = value;
                OnPropertyChanged();
            }
        }

        private string _coverLetterText;
        public string CoverLetterText
        {
            get => _coverLetterText;
            set
            {
                _coverLetterText = value;
                OnPropertyChanged();
            }
        }
        private async Task SaveAsync()
        {
            if (Application == null) return;

            if (string.IsNullOrWhiteSpace(Application.JobTitle))
            {
                await Shell.Current.DisplayAlert("Validation Error", "There must be a Job Title.", "OK");
                return; 
            }

            var apps = await _appService.LoadApplicationsAsync();
            var appToUpdate = apps.FirstOrDefault(a => a.Id == Application.Id);
            if (appToUpdate != null)
            {
                appToUpdate.JobTitle = Application.JobTitle;
                appToUpdate.JobDescription = Application.JobDescription;
                appToUpdate.ApplicationDate = Application.ApplicationDate;
                appToUpdate.Status = Application.Status;
                appToUpdate.CompanyId = Application.CompanyId;

                await _appService.SaveApplicationsAsync(apps);
            }

            if (!string.IsNullOrWhiteSpace(ResumeText))
            {
                var resumes = await _resumeService.GetResumeAsync();
                var resume = resumes.FirstOrDefault(r => r.ApplicationId == Application.Id);
                if (resume != null)
                {
                    resume.Text = ResumeText;
                    
                }
                else
                {
                    resume = new Resume
                    {
                        ApplicationId = Application.Id,
                        Text = ResumeText
                    };

                    resumes.Add(resume);
                }

                await _resumeService.SaveResumeAsync(resumes);
            }

            if(!string.IsNullOrWhiteSpace(CoverLetterText))
            {
                var coverLetters = await _coverLetterService.GetCoverLettersAsync();
                var coverLetter = coverLetters.FirstOrDefault(cl => cl.ApplicationId == Application.Id);
                if (coverLetter != null)
                {
                    coverLetter.Text = CoverLetterText;
                }
                else
                {
                    coverLetter = new CoverLetter
                    {
                        ApplicationId = Application.Id,
                        Text = CoverLetterText
                    };

                    coverLetters.Add(coverLetter);
                }
                await _coverLetterService.SaveCoverLettersAsync(coverLetters);
            }

            await Shell.Current.GoToAsync("..");
        }

        
        private async void LoadResumeAndCoverLetterAsync()
        {
            if (Application == null) return;

            var resumes = await _resumeService.GetResumeAsync();
            var coverLetters = await _coverLetterService.GetCoverLettersAsync();

            var resume = resumes.FirstOrDefault(r => r.ApplicationId == Application.Id);
            var coverLetter = coverLetters.FirstOrDefault(cl => cl.ApplicationId == Application.Id);

            ResumeText = resume?.Text ?? string.Empty;
            CoverLetterText = coverLetter?.Text ?? string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
