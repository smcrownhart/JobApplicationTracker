using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
            Routing.RegisterRoute(nameof(ApplicationDetailsPage), typeof(ApplicationDetailsPage));
            Routing.RegisterRoute(nameof(EditComapnyPage), typeof(EditComapnyPage));
            Routing.RegisterRoute(nameof(NewCompanyContactPage), typeof(NewCompanyContactPage));
            Routing.RegisterRoute(nameof(EditCompanyContactPage), typeof(EditCompanyContactPage));
        }

        
    }
}
