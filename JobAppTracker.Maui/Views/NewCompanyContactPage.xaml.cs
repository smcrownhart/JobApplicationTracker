using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(ApplicationId), "applicationId")]
public partial class NewCompanyContactPage : ContentPage
{
	private readonly NewCompanyContactViewModel _viewModel;
    public NewCompanyContactPage(NewCompanyContactViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
    }

	public string ApplicationId
    {
        set
        {
            if (int.TryParse(value, out int appId))
            {
                _viewModel.ApplicationId = appId;
            }
        }
    }
}