using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class NewInterviewViewModel : INotifyPropertyChanged
    {
        private readonly LocalInterviewStorageService _interviewStorageService;

        public NewInterviewViewModel(LocalInterviewStorageService interviewStorageService)
        {
            _interviewStorageService = interviewStorageService;
            SaveCommand = new Command(async () => await SaveAsync());
            InterviewDate = DateTime.Now;
        }

        public int ApplicationId { get; set; }

        private DateTime _interviewDate;

        public DateTime InterviewDate
        {
            get => _interviewDate;
            set
            {
                _interviewDate = value;
                OnPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get => _location; set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        private async Task SaveAsync()
        {
            var interview = new Interviews
            {
                InterviewDate = InterviewDate,
                Location = Location,
                ApplicationId = ApplicationId
            };
            await _interviewStorageService.AddInterviewAsync(interview);
            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
