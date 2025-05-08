using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class InterviewsViewModel : INotifyPropertyChanged
    {
        private readonly LocalInterviewStorageService _interviewService;

        private Interviews _interview;

        public Interviews Interview
        {
            get => _interview;
            set
            {
                _interview = value;
                OnPropertyChanged();
            }
        }
        public int ApplicationId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        public InterviewsViewModel(LocalInterviewStorageService interviewService)
        {
            _interviewService = interviewService;
            Interview = new Interviews();
            SaveCommand = new Command(async () => await SaveAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        public void LoadInterview(Interviews interview, int applicationId)
        {
            ApplicationId = applicationId;
            Interview = interview ?? new Interviews { ApplicationId = applicationId };
        }

        public async Task SaveAsync()
        {
            Interview.ApplicationId = ApplicationId;
            if (Interview.Id == 0)
            {
                await _interviewService.AddInterviewAsync(Interview);
            }
            else
            {
                await _interviewService.UpdateInterviewAsync(Interview);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async Task DeleteAsync()
        {
            if (Interview.Id != 0)
            {
                await _interviewService.DeleteInterviewAsync(Interview.Id);
            }
            await Shell.Current.GoToAsync("..");
        }
        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
