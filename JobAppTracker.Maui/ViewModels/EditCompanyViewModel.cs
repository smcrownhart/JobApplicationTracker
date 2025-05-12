using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditCompanyViewModel : INotifyPropertyChanged
    {
        private readonly LocalCompanyStorageService _companyService;

        public EditCompanyViewModel(LocalCompanyStorageService companyService)
        {
            _companyService = companyService;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public ICommand SaveCommand { get; }

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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
