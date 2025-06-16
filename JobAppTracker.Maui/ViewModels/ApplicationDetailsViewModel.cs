using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;
using JobAppTracker.Maui.Models;
using AppModel = JobApplicationTracker.DataAccess.Models.Application;

namespace JobAppTracker.Maui.ViewModels
{
    public class ApplicationDetailsViewModel : INotifyPropertyChanged
    {
        private readonly LocalApplicationStorageService _appService;
        private readonly LocalCompanyStorageService _companyService;
        private readonly LocalCompanyContactStorageService _contactService;
        private readonly LocalInterviewStorageService _interviewService;
        private readonly LocalInterviewPrepStorageService _prepService;
        private readonly LocalCheckedOnAppStorageService _checkedOnService;
        private readonly localCoverLetterStorageService _coverLetterService;
        private readonly localResumeStorageService _resumeService;
        private readonly ApplicationDeletionService _deletionService;
        private readonly INavigationHelper _navigationHelper;//This had help chatgpt
        public ApplicationDetailsViewModel(
            LocalApplicationStorageService appService,
            LocalCompanyStorageService companyService,
            LocalCompanyContactStorageService contactService,
            LocalInterviewStorageService interviewService,
            LocalInterviewPrepStorageService prepService,
            LocalCheckedOnAppStorageService checkedOnService,
            localCoverLetterStorageService coverLetterService,
            localResumeStorageService resumeService,
            ApplicationDeletionService deletionService,
            INavigationHelper navigationHelper)
        {
            _appService = appService;
            _companyService = companyService;
            _contactService = contactService;
            _interviewService = interviewService;
            _prepService = prepService;
            _checkedOnService = checkedOnService;
            _coverLetterService = coverLetterService;
            _resumeService = resumeService;
            _deletionService = deletionService;
            _navigationHelper = navigationHelper;

            //Application-New Appliation is handled elsewhere on the button on the main page
            EditCommand = new Command(async () => await EditApplicationAsync());
            DeleteApplicationCommand = new Command(async () => await DeleteApplicationAsync());

            //Resume
            ViewResumeCommand = new Command(async () => await ViewResumeAsync());
            //Cover Letter
            ViewCoverLetterCommand = new Command(async () => await ViewCoverLetterAsync());
            
            //Company--New Company is handled by application page
            EditCompanyCommand = new Command(async () => await EditCompanyAsync());

            //Contacts
            AddContactCommand = new Command(async () => await AddContactAsync());
            EditContactCommand = new Command<CompanyContact>(async(contact) => EditContactAsync(contact));

            //Interview Prep
            AddPrepCommand = new Command(async () => await AddPrepAsync());
            EditPrepCommand = new Command(async () => await EditPrepAsync());

            //Interviews
            AddInterviewCommand = new Command(async () => await AddInterviewAsync());
            EditInterviewCommand = new Command<Interviews>(async (interview) => await EditInterviewAsync());
            //CheckedOnApp
            AddCheckedCommand = new Command(async () => await AddCheckedOnAsync());

            //Navigation
            NavigateBackCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(ApplicationsPage)}"));
        }

        private AppModel _selectedApplication;
        public AppModel SelectedApplication
        {
            get => _selectedApplication;
            set
            {
                _selectedApplication = value;
                OnPropertyChanged();
            }
        }


        private Interviews _latestInterview;
        public Interviews LatestInterview
        {
            get => _latestInterview;
            set
            {
                _latestInterview = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InterviewDate));
                OnPropertyChanged(nameof(InterviewLocation));
            }
        }

        public DateTime InterviewDate => LatestInterview?.InterviewDate ?? DateTime.MinValue;
        public string InterviewLocation => LatestInterview?.Location ?? "No location";

        private DateTime _newCheckedOnDate = DateTime.Now;
        public DateTime NewCheckedOnDate
        {
            get => _newCheckedOnDate;
            set
            {
                _newCheckedOnDate = value;
                OnPropertyChanged();
            }
        }

        public Company CompanyDetails { get; set; }
        public ObservableCollection<CompanyContact> Contacts { get; set; } = new();
        public InterviewPrep PrepNotes { get; set; }
        public ObservableCollection<Interviews> Interviews { get; set; } = new();
        public ObservableCollection<CheckedOnApp> CheckedHistory { get; set; } = new();

        public ICommand EditCommand { get; }
        //Companies
        public ICommand EditCompanyCommand { get; }
        //Contacts
        public ICommand AddContactCommand { get; }
        public ICommand EditContactCommand { get; }
        //Interview Prep

        public ICommand AddPrepCommand { get; }
        public ICommand EditPrepCommand { get; }
        //Interviews
        public ICommand AddInterviewCommand { get; }
        public ICommand EditInterviewCommand { get; }
        //CheckedOnApp
        public ICommand AddCheckedCommand { get; }
        //Navigation
        public ICommand NavigateBackCommand { get; }

        //resume
        public ICommand ViewResumeCommand { get; }

        //cover letter
        public ICommand ViewCoverLetterCommand { get; }
        public async Task LoadRelatedDataAsync()
        {
            if (SelectedApplication == null) return;

            var companies = await _companyService.LoadCompaniesAsync();
            CompanyDetails = companies.FirstOrDefault(c => c.Id == SelectedApplication.CompanyId);

           

            var allcontacts = await _contactService.LoadContactsAsync();
            var matchingContacts = allcontacts.Where(c => c.ApplicationId == SelectedApplication.Id).ToList();
            Contacts = new ObservableCollection<CompanyContact>(matchingContacts);
            Contacts.Clear();
            foreach (var contact in matchingContacts)
            {
                Contacts.Add(contact);
            }
            OnPropertyChanged(nameof(Contacts));

            var prep = await _prepService.LoadPrepAsync();
            PrepNotes = prep.FirstOrDefault(p => p.ApplicationId == SelectedApplication.Id);

            var interviews = await _interviewService.LoadInterviewsAsync();
            var filtered = interviews
                .Where(i => i.ApplicationId == SelectedApplication.Id)
                .OrderByDescending(i => i.InterviewDate)
                .ToList();
            
            LatestInterview = filtered.FirstOrDefault();

            Interviews.Clear();
            foreach (var interview in filtered)
            {
                Interviews.Add(interview);
            }

            var checkedOns = await _checkedOnService.LoadCheckedOnAppsAsync();
            CheckedHistory = new ObservableCollection<CheckedOnApp>(
                checkedOns.Where(c => c.ApplicationId == SelectedApplication.Id));

            OnPropertyChanged(nameof(CompanyDetails));
            OnPropertyChanged(nameof(Contact));
            OnPropertyChanged(nameof(PrepNotes));
            OnPropertyChanged(nameof(Interviews));
            OnPropertyChanged(nameof(CheckedHistory));
        }

        private async Task ViewResumeAsync()
        {
            if (SelectedApplication == null) return;

            await Shell.Current.GoToAsync($"{nameof(ResumePage)}?applicationId={SelectedApplication.Id}");
        }

        private async Task ViewCoverLetterAsync()
        {
            if (SelectedApplication == null) return;

            await Shell.Current.GoToAsync($"{nameof(CoverLetterPage)}?applicationId={SelectedApplication.Id}");
        }

        private async Task EditApplicationAsync()
        {
            var dto = new EditApplicationDTO
            {
                Id = SelectedApplication.Id,
                JobTitle = SelectedApplication.JobTitle,
                JobDescription = SelectedApplication.JobDescription,
                ApplicationDate = SelectedApplication.ApplicationDate,
                Status = SelectedApplication.Status,
                CompanyId = SelectedApplication.CompanyId
            };

            var json = JsonSerializer.Serialize(dto);
            await Shell.Current.GoToAsync($"{nameof(EditApplicationPage)}?appJson={Uri.EscapeDataString(json)}");
        }

        private async Task EditCompanyAsync()
        {
            if (CompanyDetails == null) return;
            var json = JsonSerializer.Serialize(CompanyDetails);
            await Shell.Current.GoToAsync($"{nameof(EditComapnyPage)}?companyJson={Uri.EscapeDataString(json)}");
        }

        private async Task AddContactAsync()
        {
            if (SelectedApplication == null) return;

            await Shell.Current.GoToAsync($"{nameof(NewCompanyContactPage)}?applicationId={SelectedApplication.Id}");
        }

        private async Task EditContactAsync(CompanyContact contact)
        {
            if (contact == null)
            {
               await Shell.Current.GoToAsync($"{nameof(NewCompanyContactPage)}?applicationId={SelectedApplication.Id}");
            }
            var json = JsonSerializer.Serialize(contact);
            await Shell.Current.GoToAsync($"{nameof(EditCompanyContactPage)}?contactJson={Uri.EscapeDataString(json)}&applicationId={SelectedApplication.Id}");
        }
        private async Task AddPrepAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NewInterviewPrepPage)}?applicationId={SelectedApplication.Id}");
        }
        private async Task EditPrepAsync()
        {
            if (PrepNotes == null)
            {
                
                return;
            }

            var json = JsonSerializer.Serialize(PrepNotes);
            await Shell.Current.GoToAsync($"{nameof(EditInterviewPrepPage)}?prepJson={Uri.EscapeDataString(json)}");
        }
        

        private async Task AddInterviewAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NewInterviewPage)}?applicationId={SelectedApplication.Id}");
        }

        private async Task EditInterviewAsync()
        {

            if (SelectedApplication == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(EditInterviewPage)}?applicationId={SelectedApplication.Id}");
        }

        private async Task AddCheckedOnAsync()
        {
            var checkedOns = await _checkedOnService.LoadCheckedOnAppsAsync();

            var newChecked = new CheckedOnApp
            {
                ApplicationId = SelectedApplication.Id,
                CheckedOnDate = NewCheckedOnDate
            };

            checkedOns.Add(newChecked);
            await _checkedOnService.SaveCheckedOnAppsAsync(checkedOns);

            
            CheckedHistory.Add(newChecked);
        }

        public ICommand DeleteApplicationCommand { get; }

        private async Task DeleteApplicationAsync()
        {
            if (SelectedApplication == null) return;

            var confirm = await Shell.Current.DisplayAlert("Confirm", "Deleting Application " +
                "will delete all associated info with it", "Yes", "No");
            if (!confirm) return;

            await _deletionService.DeleteApplicationAndRelatedAsync(SelectedApplication.Id);
            await _navigationHelper.GoToAsync("//ApplicationsPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

