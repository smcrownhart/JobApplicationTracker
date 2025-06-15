using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
            Routing.RegisterRoute(nameof(EditApplicationPage), typeof(EditApplicationPage));
            Routing.RegisterRoute(nameof(ApplicationDetailsPage), typeof(ApplicationDetailsPage));
            Routing.RegisterRoute(nameof(NewCompanyPage), typeof(NewCompanyPage));
            Routing.RegisterRoute(nameof(EditComapnyPage), typeof(EditComapnyPage));
            Routing.RegisterRoute(nameof(NewCompanyContactPage), typeof(NewCompanyContactPage));
            Routing.RegisterRoute(nameof(EditCompanyContactPage), typeof(EditCompanyContactPage));
            Routing.RegisterRoute(nameof(NewInterviewPage), typeof(NewInterviewPage));
            Routing.RegisterRoute(nameof(EditInterviewPage), typeof(EditInterviewPage));
            Routing.RegisterRoute(nameof(NewInterviewPrepPage), typeof(NewInterviewPrepPage));
            Routing.RegisterRoute(nameof(EditInterviewPrepPage), typeof(EditInterviewPrepPage));
            Routing.RegisterRoute(nameof(CompaniesViewPage), typeof(CompaniesViewPage));
            Routing.RegisterRoute(nameof(ResumePage), typeof(ResumePage));
            Routing.RegisterRoute(nameof(CoverLetterPage), typeof(CoverLetterPage));
            
            

            //child routes
            Routing.RegisterRoute("applications/details", typeof(ApplicationDetailsPage));
            Routing.RegisterRoute("applications/edit", typeof(EditApplicationPage));
            Routing.RegisterRoute("applications/new", typeof(NewApplicationPage));
            Routing.RegisterRoute("applications/interviews/new", typeof(NewInterviewPage));
            Routing.RegisterRoute("applications/resumes", typeof(ResumePage));
            Routing.RegisterRoute("applications/coverletters", typeof(CoverLetterPage));
            Routing.RegisterRoute("companies/edit", typeof(EditComapnyPage));
            Routing.RegisterRoute("companies/new", typeof(NewCompanyPage));

            Navigating += OnNavigating;
        }
        private void OnNavigating(object sender, ShellNavigatingEventArgs e)
        {
            
            if (e.Target.Location.OriginalString == "..")
            {
                if (!Shell.Current.Navigation.NavigationStack.Any() || Shell.Current.Navigation.NavigationStack.Count == 1)
                {
                    
                    e.Cancel();
                    Shell.Current.GoToAsync("//MainPage");
                }
            }
        }
        //This I specifically got help from ChatGPT to implement
        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirm = await Shell.Current.DisplayAlert("Exit", "Are you sure you want to exit?", "Yes", "No");
            if (!confirm) return;

             #if ANDROID
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            #elif WINDOWS
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            #endif
        }

    }
}
