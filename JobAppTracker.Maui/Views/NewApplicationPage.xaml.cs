using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.ViewModels;
using Application = JobApplicationTracker.DataAccess.Models.Application;


namespace JobAppTracker.Maui.Views;

public partial class NewApplicationPage : ContentPage
{
    
    public NewApplicationPage(NewApplicationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    

   
}