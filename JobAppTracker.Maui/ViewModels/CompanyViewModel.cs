using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace JobAppTracker.Maui.ViewModels
{
    public class CompanyViewModel : INotifyPropertyChanged
    {
        private readonly LocalCompanyStorageService _companyService;
        private readonly CompanyDeletionService _deletionService;
      private readonly INavigationHelper _navigationHelper;

        private ObservableCollection<Company> _companies = new();

        public ObservableCollection<Company> Companies
        {
            get => _companies;
            set => SetProperty(ref _companies, value);
        }

        public ICommand LoadCompaniesCommand { get; }
        public ICommand DeleteCompanyCommand { get; }
        public ICommand AddCompanyCommand { get; }

        public CompanyViewModel(
            LocalCompanyStorageService companyService, CompanyDeletionService deletionService, INavigationHelper navigationHelper)
        {
            _companyService = companyService;
            _deletionService = deletionService;
            LoadCompaniesCommand = new Command(async () => await LoadCompaniesAsync());
            DeleteCompanyCommand = new Command<Company>(async (company) => await DeleteCompanyAsync(company));
            AddCompanyCommand = new Command(async () => await AddCompanyAsync());
            _navigationHelper = navigationHelper;
        }

        public async Task LoadCompaniesAsync()
        {
            var companies = await _companyService.LoadCompaniesAsync();
            Companies = new ObservableCollection<Company>(companies);
        }

        private async Task DeleteCompanyAsync(Company company)
        {
            if (company == null) return;

            var canDelete = await _deletionService.CanDeleteCompanyAsync(company.Id);

            if (!canDelete)
            {
                await Shell.Current.DisplayAlert("Error", "You must delete all applications associated with this company first.", "OK");
                return;
            }

            await _deletionService.DeleteCompanyAsync(company.Id);

            await LoadCompaniesAsync();
        }

        private async Task AddCompanyAsync()
        {
            await Shell.Current.GoToAsync(nameof(NewCompanyPage));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
