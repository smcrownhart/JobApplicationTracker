using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class ResumeViewModel : INotifyPropertyChanged
    {
        private readonly localResumeStorageService _resumeStorageService;
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

        public ResumeViewModel(localResumeStorageService resumeStorageService, INavigationHelper navigationHelper)
        {
            _resumeStorageService = resumeStorageService;
            CopyCommand = new Command(async () => await CopyToClipboardAsync());
            _navigationHelper = navigationHelper;

            try
            {
                NavigateBackToDetailsCommand = new Command(async () => await Shell.Current.GoToAsync($"//applications/ApplicationDetailsPage"));
            }
            catch(Exception ex)
            {
                
                Console.WriteLine($"Error initializing ResumeViewModel: {ex.Message}");
            }
            
            NavigateBackToApplicationsCommand = new Command(async () => await Shell.Current.GoToAsync($"//applications/ApplicationsPage"));
        }

        public ICommand CopyCommand { get; }
        public ICommand NavigateBackToDetailsCommand { get; }
        public ICommand NavigateBackToApplicationsCommand { get; }
        public async Task LoadResumeAsync(int applicationId)
        {
            var resumes = await _resumeStorageService.GetResumeAsync();
            var resume = resumes.FirstOrDefault(r => r.ApplicationId == applicationId);
            if(resume != null)
            {
                Text = resume.Text;
            }
            else
            {
                Text = "No Resume Added.";
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
