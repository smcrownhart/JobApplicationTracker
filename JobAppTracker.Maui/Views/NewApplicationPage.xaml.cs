using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.ViewModels;
using Application = JobApplicationTracker.DataAccess.Models.Application;


namespace JobAppTracker.Maui.Views;

public partial class NewApplicationPage : ContentPage
{
    private readonly NewApplicationViewModel _viewModel;

    public NewApplicationPage(NewApplicationViewModel viewModel)
    {
        InitializeComponent();
       _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCompaniesAsync();
    }   
}