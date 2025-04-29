using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class MainPage : ContentPage
    {
        private readonly ApplicationsPage _applicationsPage;
       

        public MainPage(ApplicationsPage applicationsPage)
        {
            InitializeComponent();
            _applicationsPage = applicationsPage;
        }

        private async void OnViewApplicationsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(_applicationsPage);
        }
    }

}
