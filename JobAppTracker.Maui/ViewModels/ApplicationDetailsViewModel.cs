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
using JobAppTracker.Maui.Views;
using System.Text.Json;

namespace JobAppTracker.Maui.ViewModels
{
    public class ApplicationDetailsViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _storageService;
        private Application _selectedApplication;

        public ApplicationDetailsViewModel(LocalApplicationStorageService storageService)
        {
            _storageService = storageService;
            UpdateCommand = new Command(async () => await UpdateApplicationAsync());
            DeleteCommand = new Command(async () => await DeleteApplicationAsync());
        }

        public Application SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged(nameof(SelectedApplication));
            }
        }
        public Command UpdateCommand { get; }
        public Command DeleteCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task DeleteApplicationAsync()
        {
            if (SelectedApplication == null)
            {
                return;
            }

            //var confirm = await Application.MainPage.DisplayAlert("Delete Application", "Are you sure you want to delete this application?", "Yes", "No");

            await _storageService.DeleteApplicationAsync(SelectedApplication.Id);
            await Shell.Current.GoToAsync("..");
        }

        private async Task UpdateApplicationAsync()
        {
            if (SelectedApplication == null)
            {
                return;
            }
            var json = JsonSerializer.Serialize(SelectedApplication, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            });
            await Shell.Current.GoToAsync($"{nameof(EditApplicationsPage)}?appJson={Uri.EscapeDataString(json)}");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
