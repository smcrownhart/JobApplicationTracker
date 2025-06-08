using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ApplicationsPage _applicationsPage;
        //private readonly localResumeStorageService _resumeService;
        //private readonly localCoverLetterStorageService _coverLetterService;
        public MainPage(ApplicationsPage applicationsPage)
        {
            InitializeComponent();
            _applicationsPage = applicationsPage;
            //_resumeService = new localResumeStorageService();
            //_coverLetterService = new localCoverLetterStorageService();

            //DeleteBadResumes();
            //DeleteBadCoverLetters();
        }

        private async void OnViewApplicationsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(_applicationsPage);
        }

        //private async void DeleteBadResumes()
        //{
        //    await _resumeService.DeleteAllResumesAsync();
        //}

        //private async void DeleteBadCoverLetters()
        //{
        //    await _coverLetterService.DeleteAllCoverLettersAsync();
        //}
    }
}
