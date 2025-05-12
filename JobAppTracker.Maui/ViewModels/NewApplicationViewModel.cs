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

        public bool CanSave => !string.IsNullOrWhiteSpace(JobTitle) && SelectedCompany != null
            && !string.IsNullOrWhiteSpace(SelectedStatus);

        public ICommand SaveCommand { get; }
        public ICommand AddCompanyCommand { get; }
        public NewApplicationViewModel(LocalApplicationStorageService appService, LocalCompanyStorageService companyService)
        {
            _appService = appService;
            _companyService = companyService;
            SaveCommand = new Command(async () => await SaveAsync(), () => CanSave);
            AddCompanyCommand = new Command(async () => await AddCompanyAsync());
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
            var application = new Application
            {
                JobTitle = JobTitle,
                JobDescription = JobDescription,
                ApplicationDate = ApplicationDate,
                Status = SelectedStatus,
                CompanyId = SelectedCompany.Id
            };
            await _appService.AddApplicationsAsync(application);
            await Shell.Current.GoToAsync("..");
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
