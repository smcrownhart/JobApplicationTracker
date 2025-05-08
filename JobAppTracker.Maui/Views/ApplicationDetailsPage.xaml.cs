using System.Text.Json;
using JobAppTracker.Maui.ViewModels;
using JobApplicationTracker.DataAccess.Models;
using Application = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(AppJson), "appJson")]
public partial class ApplicationDetailsPage : ContentPage
{
	private readonly ApplicationDetailsViewModel _viewModel;
	public ApplicationDetailsPage(ApplicationDetailsViewModel viewModel)
	{
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    public string AppJson
    {
        set
        {
            var app = JsonSerializer.Deserialize<Application>(
                Uri.UnescapeDataString(value),
                new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                });

            _viewModel.SelectedApplication = app;
            _ = _viewModel.LoadRelatedDataAsync();
        }
    }
}