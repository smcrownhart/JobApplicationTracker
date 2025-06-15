using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class CoverLetterViewModel : INotifyPropertyChanged
    {
        private readonly localCoverLetterStorageService _coverLetterStorageService;
        private readonly INavigationHelper _navigationHelper; 
        private string _text;

        public string Text
        {
            get => _text;
            set
            {

                _text = value;
                OnPropertyChanged();

            }
        }

        public CoverLetterViewModel(localCoverLetterStorageService coverLetterService, INavigationHelper navigationHelper)
        {
            _coverLetterStorageService = coverLetterService;
            CopyCommand = new Command(async () => await CopyToClipboardAsync());
            _navigationHelper = navigationHelper;
            NavigateBackToDetailsCommand = new Command(async () => await Shell.Current.GoToAsync($"//applications/ApplicationDetailsPage"));
            NavigateBackToApplicationsCommand = new Command(async () => await Shell.Current.GoToAsync($"//applications/ApplicationsPage"));
        }

        public ICommand CopyCommand { get; }
        public ICommand NavigateBackToDetailsCommand { get; }
        public ICommand NavigateBackToApplicationsCommand { get; }
        public async Task LoadCoverLetterAsync(int applicationId)
        {
            var coverLetters = await _coverLetterStorageService.GetCoverLettersAsync();
            var coverLetter = coverLetters.FirstOrDefault(c => c.ApplicationId == applicationId);
            if (coverLetter != null)
            {
                Text = coverLetter.Text;
            }
            else
            {
                Text = coverLetter?.Text ?? "No Cover Letter Added.";
            }
            
        }

        private async Task CopyToClipboardAsync()
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                await Clipboard.SetTextAsync(Text);
                await Shell.Current.DisplayAlert("Copied", "Resume copied to clipboard, now go forth and paste in your editor!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No resume content to copy.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
