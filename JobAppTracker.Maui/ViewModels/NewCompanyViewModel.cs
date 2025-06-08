using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewCompanyViewModel: INotifyPropertyChanged
    {
        private readonly LocalCompanyStorageService _companyService;
      private readonly INavigationHelper _navigationHelper;
        public NewCompanyViewModel(LocalCompanyStorageService companyService, INavigationHelper navigationHelper)
        {
            _companyService = companyService;
            SaveCommand = new Command(async () => await SaveAsync());
            AddCompanyCommand = new Command(async () => await AddCompanyAsync());
            _navigationHelper = navigationHelper;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
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
        public ICommand AddCompanyCommand { get; }



        private async Task AddCompanyAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewCompanyPage));
        }
        public ICommand SaveCommand { get; }
        

        private async Task SaveAsync()
        {
            var newCompany = new Company
            {
                Name = Name,
                Website = Website
            };
            await _companyService.AddCompanyAsync(newCompany);
            await Shell.Current.GoToAsync(".."); ;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
