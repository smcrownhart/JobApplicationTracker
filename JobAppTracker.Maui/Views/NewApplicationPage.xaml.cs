using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using Application = JobApplicationTracker.DataAccess.Models.Application;


namespace JobAppTracker.Maui.Views;

public partial class NewApplicationPage : ContentPage
{
    private readonly LocalApplicationStorageService _storageService;
    public NewApplicationPage(LocalApplicationStorageService storageService)
    {
        InitializeComponent();
        _storageService = storageService;
    }

    private void OnFieldChanged(object sender, EventArgs e)
    {
        
        SaveButton.IsEnabled = !string.IsNullOrWhiteSpace(JobTitleEntry.Text) &&
        ApplicationDatePicker.Date != default;
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var application = new Application
        {
            JobTitle = JobTitleEntry.Text,
            JobDescription = JobDescriptionEntry.Text,
            ApplicationDate = ApplicationDatePicker.Date,
            Status = StatusEntry.Text,
            
        };
        await _storageService.AddApplicationsAsync(application);
        await Shell.Current.GoToAsync("..");
    }
}