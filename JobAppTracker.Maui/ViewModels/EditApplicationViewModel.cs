using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Models;


namespace JobAppTracker.Maui.ViewModels
{
    
    public class EditApplicationViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _appService;

        public EditApplicationViewModel(LocalApplicationStorageService appService)
        {
            _appService = appService;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public ICommand SaveCommand { get; }

        private EditApplicationDTO _application;

        public EditApplicationDTO Application
        {
            get => _application;
            set
            {
                _application = value;
                OnPropertyChanged();
            }
        }
        public List<string> StatusOptions { get; } = new List<string>()
        {
            "Applied",
            "Interviewed",
            "Rejected"
        };
        private async Task SaveAsync()
        {
            if (Application == null) return;

            var apps = await _appService.LoadApplicationsAsync();
            var appToUpdate = apps.FirstOrDefault(a => a.Id == Application.Id);
            if (appToUpdate != null)
            {
                appToUpdate.JobTitle = Application.JobTitle;
                appToUpdate.JobDescription = Application.JobDescription;
                appToUpdate.ApplicationDate = Application.ApplicationDate;
                appToUpdate.Status = Application.Status;
                appToUpdate.CompanyId = Application.CompanyId;

                await _appService.SaveApplicationsAsync(apps);
            }

            await Shell.Current.GoToAsync("..");
        }

        


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
