using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
            Routing.RegisterRoute(nameof(EditComapnyPage), typeof(EditComapnyPage));
            Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
            Routing.RegisterRoute(nameof(ApplicationDetailsPage), typeof(ApplicationDetailsPage));
        }

        
    }
}
