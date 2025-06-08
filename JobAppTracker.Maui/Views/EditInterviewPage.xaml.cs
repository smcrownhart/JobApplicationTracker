using JobAppTracker.Maui.ViewModels;
using JobApplicationTracker.DataAccess.Models;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(ApplicationId), "applicationId")]
public partial class EditInterviewPage : ContentPage
{
	private readonly EditInterviewViewModel _viewModel;
	public EditInterviewPage(EditInterviewViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    private DateTime _interviewDate;
    public DateTime InterviewDate
    {
        get => _interviewDate;
        set { _interviewDate = value; OnPropertyChanged(); }
    }

    private TimeSpan _interviewTime;
    public TimeSpan InterviewTime
    {
        get => _interviewTime;
        set { _interviewTime = value; OnPropertyChanged(); }
    }

    public string Location { get; set; }

    public int Id { get; set; }
    private int _applicationId;
    public int ApplicationId
    {
        get => _applicationId;
        set
        {
            _applicationId = value;
            _ = _viewModel.LoadInterviewsAsync(_applicationId);
        }
    }
}