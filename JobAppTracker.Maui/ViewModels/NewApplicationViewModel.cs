using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using Application = JobApplicationTracker.DataAccess.Models.Application;
using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewApplicationViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _storageService;
        private readonly CompanyViewModel _companyViewModel;
        private readonly LocalCompanyStorageService _companyStorageService;

        public NewApplicationViewModel(LocalApplicationStorageService storageService, LocalCompanyStorageService companyStorageService)
        {
            _storageService = storageService;
            _companyStorageService = companyStorageService;

            SaveCommand = new Command(async () => await SaveAsync(), CanSave);
            AddCompanyCommand = new Command(async () => await AddCompanyAsync());
            LoadCompaniesAsync();
        }

        public ObservableCollection<Company> Companies { get; set; } = new();
        private Company _selectedCompany;

        public Company SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        private string _jobTitle;
        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                _jobTitle = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
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
            }
        }

        private DateTime _applicationDate = DateTime.Today;
        public DateTime ApplicatoinDate
        { get => _applicationDate;
            set {
                _applicationDate = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            } 
        }
        public string Status { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand AddCompanyCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(JobTitle) && SelectedCompany != null;
        }

        private async Task LoadCompaniesAsync()
        {
            var companies = await _companyStorageService.LoadCompaniesAsync();
            Companies.Clear();
            foreach (var company in companies)
            {
                Companies.Add(company);
            }
        }

        private async Task SaveAsync()
        {
            var app = new Application
            {
                JobTitle = JobTitle,
                JobDescription = JobDescription,
                ApplicationDate = ApplicatoinDate,
                Status = Status,
                Company = SelectedCompany,
                CompanyId =SelectedCompany.Id
            };
            await _storageService.AddApplicationsAsync(app);
            await Shell.Current.GoToAsync("..");
        }

        private async Task AddCompanyAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewCompanyPage));
        }

    }
}
