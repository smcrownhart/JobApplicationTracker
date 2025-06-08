using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewResumeViewModel : INotifyPropertyChanged
    {
        private readonly localResumeStorageService _localResumeStorageService;
      private readonly INavigationHelper _navigationHelper;
        public NewResumeViewModel(localResumeStorageService localResumeStorageService, INavigationHelper navigationHelper)
        {
            _localResumeStorageService = localResumeStorageService;
            SaveCommand = new Command(async () => await SaveResumeAsync());
            _navigationHelper = navigationHelper;
        }


        private string _resumeContent;

        public string ResumeContent
        {
            get => _resumeContent;
            set
            {
                _resumeContent = value;
                OnPropertyChanged();
            }
        }

        public int ApplicationId { get; set; }

        public ICommand SaveCommand { get; }

        private async Task SaveResumeAsync()
        {
            if (string.IsNullOrWhiteSpace(ResumeContent))
            {
                
                return;
            }
            var resume = new Resume
            {
                resumeContent = ResumeContent,
                ApplicationId = ApplicationId 
            };
            await _localResumeStorageService.AddResumeAsync(resume);
            await Shell.Current.GoToAsync(".."); ;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

