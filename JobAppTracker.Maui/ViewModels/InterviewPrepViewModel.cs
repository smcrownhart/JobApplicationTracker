using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class InterviewPrepViewModel : INotifyPropertyChanged
    {
        private readonly LocalInterviewPrepStorageService _prepService;

        private InterviewPrep _interviewPrep;

        public InterviewPrep InterviewPrep
        {
            get => _interviewPrep;
            set
            {
                _interviewPrep = value;
                OnPropertyChanged();
            }
        }

        public int ApplicationId { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public InterviewPrepViewModel(LocalInterviewPrepStorageService prepService)
        {
            _prepService = prepService;
            InterviewPrep = new InterviewPrep();
            SaveCommand = new Command(async () => await SaveAsync());
            CancelCommand = new Command(async () => await CancelAsync());
            DeleteCommand = new Command(async () => await DeleteAsync());
        }

        public void LoadInterviewPrep(InterviewPrep interviewPrep, int applicationId)
        {
            ApplicationId = applicationId;
            InterviewPrep = interviewPrep ?? new InterviewPrep { ApplicationId = applicationId };
            
        }

        private async Task SaveAsync()
        {
            if (InterviewPrep.Id == 0)
            {
                await _prepService.AddPrepAsync(InterviewPrep);
            }
            else
            {
                await _prepService.UpdatePrepAsync(InterviewPrep);
            }
            await Shell.Current.GoToAsync("..");
        }

        private async Task DeleteAsync()
        {
            if(InterviewPrep.Id == 0)
            {
                return;
            }
            if (InterviewPrep.Id != 0)
            {
                await _prepService.DeletePrepAsync(InterviewPrep.Id);
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
