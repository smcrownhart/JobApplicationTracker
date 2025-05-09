using System.Text.Json;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.ViewModels;
using Microsoft.Maui.Controls;


namespace JobAppTracker.Maui.Views;

[QueryProperty(nameof(CompanyJson), "companyJson")]
public partial class EditComapnyPage : ContentPage
{
    private readonly EditCompanyViewModel _viewModel;
    
    public EditComapnyPage(EditCompanyViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    public string CompanyJson
    {
        set
        {
            var company = JsonSerializer.Deserialize<Company>(Uri.UnescapeDataString(value));
            _viewModel.Company = company;
        }
    }

}