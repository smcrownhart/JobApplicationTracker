using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(ApplicationId), "applicationId")]
public partial class NewInterviewPage : ContentPage
{
	private readonly NewInterviewViewModel _viewModel;
    public NewInterviewPage(NewInterviewViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public string ApplicationId
    {
        set
        {
            if (int.TryParse(value, out var id))
            {
                _viewModel.ApplicationId = id;
            }
        }
    }
}