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
using Application = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _storageService;

        public ObservableCollection<Application>
            Applications { get; set; } = new ObservableCollection<Application>();

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
