using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewCompanyContactViewModel
    {
        private readonly LocalCompanyContactStorageService _contactService;

        public NewCompanyContactViewModel()
        {
            _contactService = new LocalCompanyContactStorageService();
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public ICommand SaveCommand { get; }

        private int _applicationId;

        public int ApplicationId
        {
            get => _applicationId;
            set
            {
                _applicationId = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Name)) return;

            var newContact = new CompanyContact
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                ApplicationId = ApplicationId
            };
            await _contactService.AddContactAsync(newContact);
            await Shell.Current.GoToAsync("..");

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
