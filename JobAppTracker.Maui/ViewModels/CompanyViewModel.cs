using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class CompanyViewModel
    {
        private readonly LocalCompanyStorageService _companyService;

        private Company _company;
        public Company Company
        {
            get => _company;
            set
            {
                _company = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public CompanyViewModel(LocalCompanyStorageService companyService)
        {
            _companyService = companyService;
            Company = new Company();
            SaveCommand = new Command(async () => await SaveCompanyAsync());
            DeleteCommand = new Command(async () => await DeleteCompanyAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        public void LoadCompany(Company company)
        {
            Company = company ?? new Company();
        }

        private async Task SaveCompanyAsync()
        {
            if (Company.Id == 0)
            {
                await _companyService.AddCompanyAsync(Company);
            }
            else
            {
                await _companyService.UpdateCompanyAsync(Company);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async Task DeleteCompanyAsync()
        {
            if (Company.Id == 0)
            {
                return;
            }
            if (Company.Id != 0)
            {
                await _companyService.DeleteCompanyAsync(Company.Id);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
