using System.Text.Json;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.ViewModels;
using AppModel = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(AppJson), "appJson")]
public partial class ApplicationDetails : ContentPage
{
    private AppModel _application;
    private readonly ApplicationDetailsViewModel _viewModel;
    //private readonly LocalApplicationStorageService _storageService;
    public ApplicationDetails(ApplicationDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }
    
    public string AppJson
    {
        set
        {
            var app = JsonSerializer.Deserialize<AppModel>(
             Uri.UnescapeDataString(value),
             new JsonSerializerOptions
             {
                 ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
             });
            _viewModel.SelectedApplication = app;
           
        }
    }

    

   
}