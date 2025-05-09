using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;


namespace JobAppTracker.Maui.ViewModels
{
    public class EditCompanyContactViewModel : INotifyPropertyChanged
    {
        private readonly LocalCompanyContactStorageService _contactService;

        public EditCompanyContactViewModel()
        {
            _contactService = new LocalCompanyContactStorageService();
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public ICommand SaveCommand { get; }

        private CompanyContact _contact;
        public CompanyContact Contact
        {
            get => _contact;

            set
            {
                _contact = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(Phone));
            }

        }

        public string Name
        {
            get => Contact?.Name;

            set
            {
                if (Contact != null)
                {
                    Contact.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => Contact?.Email;
            set
            {
                if (Contact != null)
                {
                    Contact.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Phone
        {
            get => Contact?.Phone;
            set
            {
                if (Contact != null)
                {
                    Contact.Phone = value;
                    OnPropertyChanged();
                }
            }
        }

        private async Task SaveAsync()
        {
            if (Contact == null) return;

            await _contactService.UpdateContactAsync(Contact);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

