using System.Text.Json;
using JobAppTracker.Maui.ViewModels;
using JobApplicationTracker.DataAccess.Models;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(PrepJson), "prepJson")]
public partial class EditInterviewPrepPage : ContentPage
{
    private readonly EditInterviewPrepViewModel _viewModel;

    public EditInterviewPrepPage(EditInterviewPrepViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public string PrepJson
    {
        set
        {
            var prep = JsonSerializer.Deserialize<InterviewPrep>(Uri.UnescapeDataString(value));
            _viewModel.Prep = prep;
        }
    }
}