using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditCompanyViewModel : INotifyPropertyChanged
    {
        private readonly LocalCompanyStorageService _companyService;
        private readonly LocalApplicationStorageService _appService;
        public EditCompanyViewModel(LocalCompanyStorageService companyService, LocalApplicationStorageService appService)
        {
            _companyService = companyService;
            _appService = appService;
            SaveCommand = new Command(async () => await SaveAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());

        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        private Company _company;

        public Company Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Website));
            }
        }

        public string Name
        {
            get => Company?.Name;
            set
            {
                if (Company != null)
                {
                    Company.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Website
        {
            get => Company?.Website;
            set
            {
                if (Company != null)
                {
                    Company.Website = value;
                    OnPropertyChanged();
                }
            }
        }

        private async Task SaveAsync()
        {
            if (Company == null) return;

            await _companyService.UpdateCompanyAsync(Company);
            await Shell.Current.GoToAsync("..");
        }

        private async Task DeleteAsync()
        {
            if (Company == null) return;
            var apps = await _appService.LoadApplicationsAsync();
            var hasApplications = apps.Any(a => a.CompanyId == Company.Id);
            if (hasApplications)
            {
                await Shell.Current.DisplayAlert("Error", "You must delete all applications you have with this company first.", "OK");
                return;
            }
            await _companyService.DeleteCompanyAsync(Company.Id);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
