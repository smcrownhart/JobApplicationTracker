using JobAppTracker.Maui.Views;
using Microsoft.Maui.Controls;

namespace JobAppTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
            Routing.RegisterRoute(nameof(ApplicationDetails), typeof(ApplicationDetails));
            Routing.RegisterRoute(nameof(EditApplicationsPage), typeof(EditApplicationsPage));
            Routing.RegisterRoute(nameof(NewCompanyPage), typeof(NewCompanyPage));
        }

        
    }
}
