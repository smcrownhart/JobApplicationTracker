using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;


namespace JobAppTracker.Maui.ViewModels
{
    public class EditCompanyContactViewModel : INotifyPropertyChanged
    {
        private readonly LocalCompanyContactStorageService _contactService;
        private readonly LocalApplicationStorageService _appService;
        private readonly INavigationHelper _navigationHelper;
        public EditCompanyContactViewModel()
        {
            _contactService = new LocalCompanyContactStorageService();
            _appService = new LocalApplicationStorageService();
            _navigationHelper = new NavigationHelper();
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public ICommand SaveCommand { get; }

        private int _applicationId;

        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                if (_applicationId != value)
                {
                    _applicationId = value;
                    OnPropertyChanged();
                    LoadContactsAsync(_applicationId);
                }
            }
        }
        public ObservableCollection<CompanyContact> Contacts { get; set; } = new();
        public async Task LoadContactsAsync(int applicationId)
        {
            var allContacts = await _contactService.LoadContactsAsync();
            var appContacts = allContacts.Where(c => c.ApplicationId == applicationId);
            Contacts.Clear();
            foreach (var contact in appContacts)
            {
                Contacts.Add(contact);
            }
        }

        private async Task SaveAsync()
        {
            //if (Contacts == null) return;

            foreach (var contact in Contacts)
            {
                await _contactService.UpdateContactAsync(contact);
            }

            var app = await _appService.GetApplicationByIdAsync(_applicationId);
            var json = JsonSerializer.Serialize(app);
            await Shell.Current.GoToAsync($"{nameof(ApplicationDetailsPage)}?appJson={Uri.EscapeDataString(json)}");

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

