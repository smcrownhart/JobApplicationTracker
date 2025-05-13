using JobAppTracker.Maui.ViewModels;
using JobApplicationTracker.DataAccess.Models;
using System.Text.Json;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(InterviewJson), "interviewJson")]
public partial class EditInterviewPage : ContentPage
{
	private readonly EditInterviewViewModel _viewModel;
	public EditInterviewPage(EditInterviewViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    public string InterviewJson
    {
        set
        {
            var interview = JsonSerializer.Deserialize<Interviews>(Uri.UnescapeDataString(value));
            _viewModel.Interview = interview;
        }
    }
}