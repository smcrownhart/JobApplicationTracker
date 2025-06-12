using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(ApplicationId), "applicationId")]
public partial class EditCompanyContactPage : ContentPage
{
    private readonly EditCompanyContactViewModel _viewModel;
    public EditCompanyContactPage(EditCompanyContactViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    private int _applicationId;
    public int ApplicationId
    {
        get => _applicationId;
        set
        {
            _applicationId = value;
            _viewModel.ApplicationId = _applicationId;
            _ = _viewModel.LoadContactsAsync(_applicationId);
        }
    }

    
}