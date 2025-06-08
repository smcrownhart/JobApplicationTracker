using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
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
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

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

    }
}
