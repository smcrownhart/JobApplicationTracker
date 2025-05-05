using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class CompanyViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _storageService;
        private ObservableCollection<Company> _companies = new();

        public ObservableCollection<Company> Companies
        {
            get => _companies;
            set
            {
                _companies = value;
                OnPropertyChanged();
            }
        }

        public CompanyViewModel(LocalApplicationStorageService storageService)
        {
            _storageService = storageService;
           
        }

        public async Task LoadCompaniesAsync()
        {
            var applications = await _storageService.LoadApplicationsAsync();
            var companies = applications.Select(a => a.Company).Distinct().ToList();
            Companies = new ObservableCollection<Company>(companies);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
