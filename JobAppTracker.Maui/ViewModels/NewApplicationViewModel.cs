using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;
using Application = JobApplicationTracker.DataAccess.Models.Application;


namespace JobAppTracker.Maui.ViewModels
{
    public class NewApplicationViewModel: INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _appService;
        private readonly LocalCompanyStorageService _companyService;
        private readonly localResumeStorageService _resumeService;
        private readonly localCoverLetterStorageService _coverLetterService;
      private readonly INavigationHelper _navigationHelper;
        private string _jobTitle;
        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                _jobTitle = value;
                OnPropertyChanged();
                TriggerValidation();
            }
        }

        private string _jobDescription;
        public string JobDescription
        {
            get => _jobDescription;
            set
            {
                _jobDescription = value;
                OnPropertyChanged();
                TriggerValidation();
            }
        }

        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
                TriggerValidation();
            }
        }

        public ObservableCollection<Company> Companies { get; set; } = new ObservableCollection<Company>();

        public List<string> Statuses { get; } = new List<string>
        {
            "Applied",
            "Interviewed",
            "Rejected"
        };

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
                TriggerValidation();
            }
        }
        private string _resumeText;
        public string ResumeText
        {
            get => _resumeText;
            set
            {
                _resumeText = value;
                OnPropertyChanged();
                TriggerValidation();
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
                TriggerValidation();
            }
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(JobTitle) && SelectedCompany != null
            && !string.IsNullOrWhiteSpace(SelectedStatus);

        public ICommand SaveCommand { get; }
        public ICommand AddCompanyCommand { get; }
        public NewApplicationViewModel(LocalApplicationStorageService appService, LocalCompanyStorageService companyService,
            localResumeStorageService resumeService, localCoverLetterStorageService coverletterService, INavigationHelper navigationHelper)
        {
            _appService = appService;
            _companyService = companyService;
            _resumeService = resumeService; ;
            _coverLetterService = coverletterService;
            SaveCommand = new Command(async () => await SaveAsync(), () => CanSave);
            AddCompanyCommand = new Command(async () => await AddCompanyAsync());
            _navigationHelper = navigationHelper;
        }

        public async Task LoadCompaniesAsync()
        {
            var companies = await _companyService.LoadCompaniesAsync();
            Companies.Clear();
            foreach (var company in companies)
            {
                Companies.Add(company);
            }

            SelectedCompany = Companies.OrderByDescending(c => c.Id).FirstOrDefault();
        }

        private async Task SaveAsync()
        {
            if(!CanSave)
                return;
            var existingApplications = await _appService.LoadApplicationsAsync();
            bool duplicateExists = existingApplications.Any(a =>
                a.JobTitle.Equals(JobTitle, StringComparison.OrdinalIgnoreCase) &&
                a.CompanyId == SelectedCompany.Id);

            if (duplicateExists)
            {
                await Shell.Current.DisplayAlert(
                    "An Application for this job already exists",
                    "Delete the previous application to add another one for the same job",
                    "OK"
                );
                return;
            }

            var application = new Application
            {
                JobTitle = JobTitle,
                JobDescription = JobDescription,
                ApplicationDate = ApplicationDate,
                CompanyId = SelectedCompany.Id,
                Status = SelectedStatus
            };
            
            
            application = await _appService.AddApplicationsAsync(application);

            if (!string.IsNullOrWhiteSpace(ResumeText))
            {
                var resume = new Resume
                {
                    ApplicationId = application.Id,
                    Text = ResumeText
                };
                await _resumeService.AddResumeAsync(resume);
            }

           if (!string.IsNullOrWhiteSpace(CoverLetterText))
            {
                var coverLetter = new CoverLetter
                {
                    ApplicationId = application.Id,
                    Text = CoverLetterText
                };

                await _coverLetterService.AddCoverLetterAsync(coverLetter);
            }

           
            var json = JsonSerializer.Serialize(application);

            JobTitle = string.Empty;
            JobDescription = string.Empty;
            SelectedCompany = null;
            SelectedStatus = null;
            ResumeText = string.Empty;
            CoverLetterText = string.Empty;
            TriggerValidation();
            await Shell.Current.GoToAsync($"{nameof(ApplicationDetailsPage)}?appJson={Uri.EscapeDataString(json)}");

           
        }

        private async Task AddCompanyAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewCompanyPage));
        }

        private void TriggerValidation()
        {
            OnPropertyChanged(nameof(CanSave));
            ((Command)SaveCommand).ChangeCanExecute();
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
