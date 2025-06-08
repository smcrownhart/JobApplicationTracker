using JobAppTracker.Maui.ViewModels;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace JobAppTracker.Maui.Views
{
    [QueryProperty(nameof(ApplicationId), "applicationId")]
    public partial class CoverLetterPage : ContentPage
    {
        private readonly CoverLetterViewModel _viewModel;

        public CoverLetterPage(CoverLetterViewModel viewModel)
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
                _ = _viewModel.LoadCoverLetterAsync(_applicationId);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadCoverLetterAsync(ApplicationId);
        }
    }
}