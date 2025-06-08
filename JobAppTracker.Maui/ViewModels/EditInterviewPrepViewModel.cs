using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditInterviewPrepViewModel : INotifyPropertyChanged
    {
        private readonly LocalInterviewPrepStorageService _prepService;
      private readonly INavigationHelper _navigationHelper;
        public EditInterviewPrepViewModel(LocalInterviewPrepStorageService prepService, INavigationHelper navigationHelper)
        {
            _prepService = prepService;
            SaveCommand = new Command(async () => await SaveAsync());
            _navigationHelper = navigationHelper;
        }

        private InterviewPrep _prep;
        public InterviewPrep Prep
        {
            get => _prep;
            set
            {
                _prep = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PrepNotes));
                OnPropertyChanged(nameof(CompanyNotes));
            }
        }

        public string PrepNotes
        {
            get => Prep?.PrepNotes;
            set
            {
                if (Prep != null)
                {
                    Prep.PrepNotes = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CompanyNotes
        {
            get => Prep?.CompanyNotes;
            set
            {
                if (Prep != null)
                {
                    Prep.CompanyNotes = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async Task SaveAsync()
        {
            if (Prep == null) return;

            var preps = await _prepService.LoadPrepAsync();
            var existing = preps.FirstOrDefault(p => p.Id == Prep.Id);
            if (existing != null)
            {
                existing.PrepNotes = Prep.PrepNotes;
                existing.CompanyNotes = Prep.CompanyNotes;
                await _prepService.SavePrepAsync(preps);
            }

            await Shell.Current.GoToAsync(".."); ;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}