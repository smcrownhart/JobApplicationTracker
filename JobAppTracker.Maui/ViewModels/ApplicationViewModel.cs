using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using AppModel= JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _storageService;
        private ObservableCollection<AppModel> _filteredApplications = new();

        public ObservableCollection<AppModel>
            Applications { get; set; } = new ObservableCollection<AppModel>();

        public ObservableCollection<AppModel> FilteredApplications
        {
            get => _filteredApplications;
            set
            {
                _filteredApplications = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter(_searchText);
            }
        }

        public ApplicationViewModel(LocalApplicationStorageService storageService)
        {
            _storageService = storageService;
            LoadApplicationsCommand = new Command(async () => await LoadApplicationsAsync());
            
        }

        public Command LoadApplicationsCommand { get; }

        public async Task LoadApplicationsAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var applications = await _storageService.LoadApplicationsAsync();
                Applications.Clear();
                foreach (var app in applications)
                {
                    Applications.Add(app);
                }

                ApplyFilter(SearchText);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to load applications",  ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        

        public void ApplyFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                FilteredApplications = new ObservableCollection<AppModel>(Applications);
            }
            else
            {
                filter = filter.ToLower();
                var filtered = Applications.Where(a =>
                    a.JobTitle?.ToLower().Contains(filter) == true ||
                    a.Company?.Name?.ToLower().Contains(filter) == true
                );

                FilteredApplications = new ObservableCollection<AppModel>(filtered);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
