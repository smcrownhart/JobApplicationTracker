using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobAppTracker.Maui.Services;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;


namespace JobAppTracker.Maui.ViewModels
{
    public class NewCompanyViewModel : INotifyPropertyChanged
    {
        private readonly LocalCompanyStorageService _companyStorageService;

        public NewCompanyViewModel(LocalCompanyStorageService companyStorageService)
        {
            _companyStorageService = companyStorageService;
            SaveCommand = new Command(async () => await SaveCompanyAsync(), () => CanSave);
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        private string _website;

        public string Website
        {
            get => _website;
            set
            {
                _website = value;
                OnPropertyChanged();
            }
        }
        public ICommand SaveCommand { get; }

        public bool CanSave => !string.IsNullOrWhiteSpace(Name);

        private async Task SaveCompanyAsync()
        {
            var company = new Company
            {
                Name = this.Name,
                Website = this.Website
            };
            await _companyStorageService.AddCompanyAsync(company);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
