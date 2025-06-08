using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditCoverLetterViewModel : INotifyPropertyChanged
    {
        private readonly localCoverLetterStorageService _coverLetterService;
      private readonly INavigationHelper _navigationHelper;
        public EditCoverLetterViewModel(localCoverLetterStorageService coverLetterService, INavigationHelper navigationHelper)
        {
            _coverLetterService = coverLetterService;
            SaveCommand = new Command(async () => await SaveAsync());
            _navigationHelper = navigationHelper;
        }

        private CoverLetter _coverLetter;
        public CoverLetter CoverLetter
        {
            get => _coverLetter;
            set
            {
                _coverLetter = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Text));
            }
        }

        public string Text
        {
            get => CoverLetter?.Text;
            set
            {
                if (CoverLetter != null)
                {
                    CoverLetter.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async Task SaveAsync()
        {
            if (CoverLetter != null)
            {
                await _coverLetterService.UpdateCoverLetterAsync(CoverLetter);
                await Shell.Current.GoToAsync(".."); ;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}