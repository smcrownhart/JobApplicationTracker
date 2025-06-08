using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditResumeViewModel : INotifyPropertyChanged
    {
        private readonly localResumeStorageService _resumeStorage;
      private readonly INavigationHelper _navigationHelper;
        public EditResumeViewModel(localResumeStorageService resumeStorage, INavigationHelper navigationHelper)
        {
            _resumeStorage = resumeStorage;
            SaveCommand = new Command(async () => await SaveResumeAsync());
            _navigationHelper = navigationHelper;
        }

        private Resume _resume;
        public Resume Resume
        {
            get => _resume;
            set
            {
                _resume = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Text));
            }
        }

        public string Text
        {
            get => Resume?.Text;
            set
            {
                if (Resume != null)
                {
                    Resume.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async Task SaveResumeAsync()
        {
            if (Resume != null)
            {
                await _resumeStorage.UpdateResumeAsync(Resume);
                await Shell.Current.GoToAsync(".."); ;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
