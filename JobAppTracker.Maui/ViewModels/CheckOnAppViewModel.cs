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
    public class CheckOnAppViewModel:INotifyPropertyChanged
    {

        private readonly LocalCheckedOnAppStorageService _checkOnService;
      private readonly INavigationHelper _navigationHelper;

        private CheckedOnApp _entry;
        public CheckedOnApp CheckedOnApp
        {
            get => _entry;
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }

        public int ApplicationId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public CheckOnAppViewModel(LocalCheckedOnAppStorageService checkOnService, INavigationHelper navigationHelper)
        {
            _checkOnService = checkOnService;
            CheckedOnApp = new CheckedOnApp { CheckedOnDate = DateTime.Today };
            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await CancelAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
            _navigationHelper = navigationHelper;
        }

        public void LoadCheckedOnApp(CheckedOnApp checkedOnApp, int applicationId)
        {
            ApplicationId = applicationId;
            CheckedOnApp = checkedOnApp ?? new CheckedOnApp { ApplicationId = applicationId, CheckedOnDate = DateTime.Today };
        }

        private async Task SaveAsync()
        {
            CheckedOnApp.ApplicationId = ApplicationId; 
            if (CheckedOnApp.Id == 0)
            {
                await _checkOnService.AddCheckedOnAppAsync(CheckedOnApp);
            }
            else
            {
                await _checkOnService.UpdateCheckedOnAppAsync(CheckedOnApp);
            }
            await _navigationHelper.GoToAsync("//MainPage");
        }

        private async Task DeleteAsync()
        {
            if (CheckedOnApp.Id != 0)
            {
                await _checkOnService.DeleteCheckedOnAppAsync(CheckedOnApp.Id);
            }
            await _navigationHelper.GoToAsync("//MainPage");
        }
        private async Task CancelAsync()
        {
            await _navigationHelper.GoToAsync("//MainPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
