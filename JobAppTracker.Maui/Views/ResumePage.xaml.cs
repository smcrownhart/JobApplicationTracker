using JobAppTracker.Maui.ViewModels;
using Microsoft.Maui.Controls;
using System.Threading.Tasks; 

namespace JobAppTracker.Maui.Views
{
    [QueryProperty(nameof(ApplicationId), "applicationId")]
    public partial class ResumePage : ContentPage
    {
        private readonly ResumeViewModel _viewModel;


        public ResumePage(ResumeViewModel viewModel)
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
                _ = _viewModel.LoadResumeAsync(_applicationId);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadResumeAsync(ApplicationId);
        }
    }
}