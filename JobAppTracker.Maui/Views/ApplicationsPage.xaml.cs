using System.Text.Json;
using JobAppTracker.Maui.ViewModels;
using AppModel = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.Views;

public partial class ApplicationsPage : ContentPage
{
    public ApplicationsPage(ApplicationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ApplicationViewModel viewModel)
        {
            await viewModel.LoadApplicationsAsync();
            viewModel.ApplyFilter(viewModel.SearchText);
        }
    }

    private async void OnAddApplicationButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewApplicationPage));
    }

    private async void OnApplicationSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AppModel selectedApplication)
        {
            if(selectedApplication.JobTitle == "No applications found")
            {
                await Shell.Current.GoToAsync("//ApplicationsPage");
                return;
            }

            var viewModel = BindingContext as ApplicationViewModel;
            if (viewModel != null && viewModel.FilteredApplications.Contains(selectedApplication))
            {
                var json = JsonSerializer.Serialize(selectedApplication);
                await Shell.Current.GoToAsync($"{nameof(ApplicationDetailsPage)}?appJson={Uri.EscapeDataString(json)}");
            }
        }

        ((CollectionView)sender).SelectedItem = null;
    }
}