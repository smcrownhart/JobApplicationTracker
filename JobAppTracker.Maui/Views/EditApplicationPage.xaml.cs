using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.ViewModels;
using JobAppTracker.Maui.Models;
using AppModel = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(AppJson), "appJson")]
public partial class EditApplicationPage : ContentPage
{
    private readonly EditApplicationViewModel _viewModel;
    public EditApplicationPage(EditApplicationViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public string AppJson
    {
        set
        {
            var app = JsonSerializer.Deserialize<EditApplicationDTO>(Uri.UnescapeDataString(value));
            _viewModel.Application = app;

        }
    }
}