using System.Text.Json;
using JobAppTracker.Maui.ViewModels;
using AppModel = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.Views;
[QueryProperty(nameof(AppJson), "appJson")]
public partial class EditApplicationsPage : ContentPage
{
	private AppModel _application;

    public AppModel Application
    {
        get => _application;
        set
        {
            _application = value;
            OnPropertyChanged();
        }
    }
    public EditApplicationsPage(EditApplicationsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public string AppJson
	{
		set
		{
			var app = JsonSerializer.Deserialize<AppModel>(Uri.UnescapeDataString(value));
			if (BindingContext is EditApplicationsViewModel viewModel)
            {
                viewModel.Application = app;
            }
        }
	}
}