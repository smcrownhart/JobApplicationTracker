using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewInterviewPrepViewModel : INotifyPropertyChanged
    {
        private readonly LocalInterviewPrepStorageService _prepService;

        public NewInterviewPrepViewModel(LocalInterviewPrepStorageService prepService)
        {
            _prepService = prepService;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        public int ApplicationId { get; set; }

        private string _prepNotes;
        public string PrepNotes
        {
            get => _prepNotes;
            set
            {
                _prepNotes = value;
                OnPropertyChanged();
            }
        }

        private string _companyNotes;
        public string CompanyNotes
        {
            get => _companyNotes;
            set
            {
                _companyNotes = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(PrepNotes) && string.IsNullOrWhiteSpace(CompanyNotes)) return;

            var preps = await _prepService.LoadPrepAsync();

            var newPrep = new InterviewPrep
            {
                PrepNotes = PrepNotes,
                CompanyNotes = CompanyNotes,
                ApplicationId = ApplicationId
            };

            preps.Add(newPrep);
            await _prepService.SavePrepAsync(preps);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
