using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

public partial class NewCompanyPage : ContentPage
{
	public NewCompanyPage(NewCompanyViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}