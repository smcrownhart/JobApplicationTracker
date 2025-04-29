using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

public partial class ApplicationsPage : ContentPage
{
	public ApplicationsPage(ApplicationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

	}

    private async void OnAddApplicationButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewApplicationPage));
    }

}