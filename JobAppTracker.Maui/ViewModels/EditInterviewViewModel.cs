using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;

namespace JobAppTracker.Maui.ViewModels
{
    public class EditInterviewViewModel: INotifyPropertyChanged
    {
        private readonly LocalInterviewStorageService _interviewStorageService;

        public EditInterviewViewModel(LocalInterviewStorageService interviewStorageService)
        {
            _interviewStorageService = interviewStorageService;
            SaveCommand = new Command(async () => await SaveAsync());
        }

        private Interviews _interview;

        public Interviews Interview
        {
            get => _interview;
            set
            {
                _interview = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InterviewDate));
                OnPropertyChanged(nameof(Location));
            }
        }

        public DateTime InterviewDate
        {
            get => Interview?.InterviewDate ?? DateTime.Now;
            set
            {
                if (Interview != null)
                {
                    Interview.InterviewDate = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public string Location
        {
            get => Interview?.Location;
            set
            {
                if (Interview != null)
                {
                    Interview.Location = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        private async Task SaveAsync()
        {
            if (Interview != null)
            {
                await _interviewStorageService.UpdateInterviewAsync(Interview);
                await Shell.Current.GoToAsync("..");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
