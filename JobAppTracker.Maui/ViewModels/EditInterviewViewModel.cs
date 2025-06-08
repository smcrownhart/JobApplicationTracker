using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
      private readonly INavigationHelper _navigationHelper;
        public ObservableCollection<EditInterviewItem> Interviews { get; set; } = new();
        public EditInterviewViewModel(LocalInterviewStorageService interviewStorageService, INavigationHelper navigationHelper)
        {
            _interviewStorageService = interviewStorageService;
            SaveCommand = new Command(async () => await SaveAsync());
            _navigationHelper = navigationHelper;
        }

        private int _applicationId;

        public async Task LoadInterviewsAsync(int applicationId)
        {
            _applicationId = applicationId;
            var allInterviews = await _interviewStorageService.LoadInterviewsAsync();
            var appInterviews = allInterviews.Where(i => i.ApplicationId == _applicationId);

            Interviews.Clear();
            foreach (var interview in appInterviews)
            {
                var editable = new EditInterviewItem(interview.Id, interview.ApplicationId)
                {
                    InterviewDate = interview.InterviewDate.Date,
                    InterviewTime = interview.InterviewDate.TimeOfDay,
                    Location = interview.Location
                };
                

                Interviews.Add(editable);

            }
            OnPropertyChanged(nameof(Interviews));
        }

        public ICommand SaveCommand { get; }

        private async Task SaveAsync()
        {
            foreach (var editable in Interviews)
            {
                var interview = new Interviews
                {
                    Id = editable.Id,
                    ApplicationId = editable.ApplicationId,
                    Location = editable.Location,
                    InterviewDate = editable.InterviewDate.Date + editable.InterviewTime
                };
                await _interviewStorageService.UpdateInterviewAsync(interview);
            }
            await Shell.Current.GoToAsync(".."); ;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class EditInterviewItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }

        public EditInterviewItem(int id, int applicationId)
        {
            Id = id;
            ApplicationId = applicationId;
        }

        private DateTime _interviewDate;
        public DateTime InterviewDate
        {
            get => _interviewDate;
            set { _interviewDate = value; OnPropertyChanged(); }
        }

        private TimeSpan _interviewTime;
        public TimeSpan InterviewTime
        {
            get => _interviewTime;
            set { _interviewTime = value; OnPropertyChanged(); }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set { _location = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
