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

        public ApplicationDetailsViewModel(
            LocalApplicationStorageService appService,
            LocalCompanyStorageService companyService,
            LocalCompanyContactStorageService contactService,
            LocalInterviewStorageService interviewService,
            LocalInterviewPrepStorageService prepService,
            LocalCheckedOnAppStorageService checkedOnService)
        {
            _appService = appService;
            _companyService = companyService;
            _contactService = contactService;
            _interviewService = interviewService;
            _prepService = prepService;
            _checkedOnService = checkedOnService;
            
            //Application-New Appliation is handled elsewhere on the button on the main page
            EditCommand = new Command(async () => await EditApplicationAsync());

            //Company--New Company is handled by application page
            EditCompanyCommand = new Command(async () => await EditCompanyAsync());

            //Contacts
            AddContactCommand = new Command(async () => await AddContactAsync());
            EditContactCommand = new Command(async () => await EditContactAsync());

            //Interview Prep
            AddPrepCommand = new Command(async () => await AddPrepAsync());
            EditPrepCommand = new Command(async () => await EditPrepAsync());
            
            //Interviews
            AddInterviewCommand = new Command(async () => await AddInterviewAsync());
            EditInterviewCommand = new Command<Interviews>(async (interview) => await EditInterviewAsync());
            //CheckedOnApp
            AddCheckedCommand = new Command(async () => await AddCheckedOnAsync());
        }

        public AppModel SelectedApplication { get; set; }

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

        public Company CompanyDetails { get; set; }
        public CompanyContact Contact { get; set; }
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

        public async Task LoadRelatedDataAsync()
        {
            if (SelectedApplication == null) return;

            var companies = await _companyService.LoadCompaniesAsync();
            CompanyDetails = companies.FirstOrDefault(c => c.Id == SelectedApplication.CompanyId);
           

            var contacts = await _contactService.LoadContactsAsync();
            Contact = contacts.FirstOrDefault(c => c.ApplicationId == SelectedApplication.Id);
            

            var prep = await _prepService.LoadPrepAsync();
            PrepNotes = prep.FirstOrDefault(p => p.ApplicationId == SelectedApplication.Id);

            var interviews = await _interviewService.LoadInterviewsAsync();
            LatestInterview = interviews
                .Where(i => i.ApplicationId == SelectedApplication.Id)
                .OrderByDescending(i => i.InterviewDate)
                .FirstOrDefault();

            var checkedOns = await _checkedOnService.LoadCheckedOnAppsAsync();
            CheckedHistory = new ObservableCollection<CheckedOnApp>(
                checkedOns.Where(c => c.ApplicationId == SelectedApplication.Id));

            OnPropertyChanged(nameof(CompanyDetails));
            OnPropertyChanged(nameof(Contact));
            OnPropertyChanged(nameof(PrepNotes));
            OnPropertyChanged(nameof(Interviews));
            OnPropertyChanged(nameof(CheckedHistory));
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

        private async Task EditContactAsync()
        {
            if (Contact == null)
            {
               await Shell.Current.GoToAsync($"{nameof(NewCompanyContactPage)}?applicationId={SelectedApplication.Id}");
            }
            var json = JsonSerializer.Serialize(Contact);
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
            await Shell.Current.DisplayAlert("Debug", "EditInterviewAsync called!", "OK");
            if (LatestInterview == null)
            {
                
                return;
            }
            LatestInterview.Application = null;
            var json = JsonSerializer.Serialize(LatestInterview);
            await Shell.Current.GoToAsync($"{nameof(EditInterviewPage)}?interviewJson={Uri.EscapeDataString(json)}");
        }

        private async Task AddCheckedOnAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NewCheckedOnAppPage)}?applicationId={SelectedApplication.Id}");
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

