using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(ApplicationId), "applicationId")]
public partial class NewInterviewPrepPage : ContentPage
{
    private readonly NewInterviewPrepViewModel _viewModel;

    public NewInterviewPrepPage(NewInterviewPrepViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public string ApplicationId
    {
        set
        {
            if (int.TryParse(value, out var appId))
            {
                _viewModel.ApplicationId = appId;
            }
        }
    }
}
