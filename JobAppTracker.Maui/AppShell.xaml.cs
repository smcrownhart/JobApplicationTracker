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
        }

        
    }
}
