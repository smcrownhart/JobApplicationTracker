using System.Text.Json;
using JobAppTracker.Maui.ViewModels;
using JobApplicationTracker.DataAccess.Models;
using AppModel = JobApplicationTracker.DataAccess.Models.Application;

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
        //set
        //{
        //    var app = JsonSerializer.Deserialize<AppModel>(Uri.UnescapeDataString(value));
        //    _viewModel.SelectedApplication = app;
        //    _ = _viewModel.LoadRelatedDataAsync();
        //}
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                return; // Prevent crash when no value is passed

            var app = JsonSerializer.Deserialize<AppModel>(Uri.UnescapeDataString(value));
            if (BindingContext is ApplicationDetailsViewModel vm)
            {
                vm.SelectedApplication = app;
                _ = vm.LoadRelatedDataAsync();
            }
        }

    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    if (BindingContext is ApplicationDetailsViewModel vm)
    //    {
    //        await vm.LoadRelatedDataAsync();
    //    }
    //}
}