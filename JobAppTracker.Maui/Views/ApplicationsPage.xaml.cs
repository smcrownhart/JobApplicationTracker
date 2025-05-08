using System.Text.Json;
using JobAppTracker.Maui.ViewModels;

namespace JobAppTracker.Maui.Views;

public partial class ApplicationsPage : ContentPage
{
    public ApplicationsPage(ApplicationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }

    private async void OnAddApplicationButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NewApplicationPage));
    }

    private async void OnApplicationSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Application selectedApplication)
        {
            var json = JsonSerializer.Serialize(selectedApplication, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            });

            await Shell.Current.GoToAsync($"{nameof(ApplicationDetailsPage)}?appJson={Uri.EscapeDataString(json)}");
        }

        ((CollectionView)sender).SelectedItem = null; // Deselect the item

    }
}