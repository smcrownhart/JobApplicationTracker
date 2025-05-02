using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using System.Windows.Input;
using Application = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditApplicationsViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _storageService;

        private Application _application;
        public Application Application
        {
            get => _application;
            set
            {
                _application = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public Command DeleteCommand { get; }
        public EditApplicationsViewModel(LocalApplicationStorageService storageService)
        {
            _storageService = storageService;
            SaveCommand = new Command(async () => await SaveAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
        }

        public async Task SaveAsync()
        {
            await _storageService.UpdateApplicationAsync(Application);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public async Task DeleteAsync()
        {
            if (Application == null)
            {
                return;
            }

            bool confirm = await Shell.Current.DisplayAlert(
                "Delete Application",
                $"Are you sure you want to delete the application for {Application.JobTitle}?",
                "Yes",
                "No");
            if (!confirm)
            {
                return;
            }
            await _storageService.DeleteApplicationAsync(Application.Id);
            await Shell.Current.GoToAsync("..");
        }

    }

    
}
