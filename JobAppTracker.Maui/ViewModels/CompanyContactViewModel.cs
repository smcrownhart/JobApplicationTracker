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
    public class CompanyContactViewModel: INotifyPropertyChanged
    {
        private readonly LocalCompanyContactStorageService _contactService;

        private CompanyContact _contact;

        public CompanyContact Contact
        {
            get => _contact;
            set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }

        public int ApplicationId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public CompanyContactViewModel(LocalCompanyContactStorageService contactService)
        {
            _contactService = contactService;
            Contact = new CompanyContact();
            SaveCommand = new Command(async () => await SaveAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        public void LoadContact(CompanyContact contact, int applicationId)
        {
            ApplicationId = applicationId;
            Contact = contact ?? new CompanyContact { ApplicationId = applicationId };
        }

        private async Task SaveAsync()
        {
            Contact.ApplicationId = ApplicationId;

            if (Contact.Id == 0)
            {
                await _contactService.AddContactAsync(Contact);
            }
            else
            {
                await _contactService.UpdateContactAsync(Contact);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async Task DeleteAsync()
        {
            if (Contact.Id == 0)
            {
                return;
            }
                
            if (Contact.Id != 0)
            {
                await _contactService.DeleteContactAsync(Contact.Id);
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
