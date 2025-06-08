using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewCoverLetterViewModel : INotifyPropertyChanged
    {
        private readonly localCoverLetterStorageService _localCoverLetterStorageService;
      private readonly INavigationHelper _navigationHelper;
        public NewCoverLetterViewModel(localCoverLetterStorageService localCoverLetterStorageService, INavigationHelper navigationHelper)
        {
            _localCoverLetterStorageService = localCoverLetterStorageService;
            SaveCommand = new Command(async () => await SaveCoverLetterAsync());
            _navigationHelper = navigationHelper;
        }

        private string _coverLetterContent;

        public string CoverLetterContent
        {
            get => _coverLetterContent;
            set
            {
                _coverLetterContent = value;
                OnPropertyChanged();
            }
        }

        public int ApplicationId { get; set; }

        public ICommand SaveCommand { get; }

        private async Task SaveCoverLetterAsync()
        {
            if (string.IsNullOrWhiteSpace(CoverLetterContent))
            {
                return;
            }
            var coverLetter = new CoverLetter
            {
                letterContent = CoverLetterContent,
                ApplicationId = ApplicationId
            };
            await _localCoverLetterStorageService.AddCoverLetterAsync(coverLetter);
            await Shell.Current.GoToAsync(".."); ;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

