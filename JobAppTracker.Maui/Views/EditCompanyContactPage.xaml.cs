using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(ContactJson), "contactJson")]
public partial class EditCompanyContactPage : ContentPage
{
    private readonly EditCompanyContactViewModel _viewModel;
    public EditCompanyContactPage(EditCompanyContactViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    public string ContactJson
    {
        set
        {
            var contact = JsonSerializer.Deserialize<CompanyContact>(Uri.UnescapeDataString(value));
            _viewModel.Contact = contact;
        }
    }
}