using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

public partial class CompaniesViewPage : ContentPage
{
    private readonly CompanyViewModel _viewModel;
    public CompaniesViewPage(CompanyViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
       
        
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCompaniesAsync();
    }
}