using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewApplicationPage), typeof(NewApplicationPage));
            Routing.RegisterRoute(nameof(EditApplicationsPage), typeof(EditApplicationsPage));
        }

        
    }
}
