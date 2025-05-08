using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;
using Application = JobApplicationTracker.DataAccess.Models.Application;

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

            EditCommand = new Command(async () => await EditApplicationAsync());
            EditCompanyCommand = new Command(async () => await EditCompanyAsync());
            EditContactCommand = new Command(async () => await EditContactAsync());
            EditPrepCommand = new Command(async () => await EditPrepAsync());
            AddInterviewCommand = new Command(async () => await AddInterviewAsync());
            AddCheckedCommand = new Command(async () => await AddCheckedOnAsync());
        }

        public Application SelectedApplication { get; set; }

        public Company CompanyDetails { get; set; }
        public CompanyContact Contact { get; set; }
        public InterviewPrep PrepNotes { get; set; }
        public ObservableCollection<Interviews> Interviews { get; set; } = new();
        public ObservableCollection<CheckedOnApp> CheckedHistory { get; set; } = new();

        public ICommand EditCommand { get; }
        public ICommand EditCompanyCommand { get; }
        public ICommand EditContactCommand { get; }
        public ICommand EditPrepCommand { get; }
        public ICommand AddInterviewCommand { get; }
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
            Interviews = new ObservableCollection<Interviews>(
                interviews.Where(i => i.ApplicationId == SelectedApplication.Id));

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
            var json = JsonSerializer.Serialize(SelectedApplication, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            });
            await Shell.Current.GoToAsync($"{nameof(EditApplicationPage)}?appJson={Uri.EscapeDataString(json)}");
        }

        private async Task EditCompanyAsync()
        {
            var json = JsonSerializer.Serialize(CompanyDetails);
            await Shell.Current.GoToAsync($"{nameof(EditComapnyPage)}?companyJson={Uri.EscapeDataString(json)}");
        }

        private async Task EditContactAsync()
        {
            var json = JsonSerializer.Serialize(Contact);
            await Shell.Current.GoToAsync($"{nameof(NewCompanyContactPage)}?contactJson={Uri.EscapeDataString(json)}&applicationId={SelectedApplication.Id}");
        }

        private async Task EditPrepAsync()
        {
            var json = JsonSerializer.Serialize(PrepNotes);
            await Shell.Current.GoToAsync($"{nameof(NewInterviewPrepPage)}?prepJson={Uri.EscapeDataString(json)}&applicationId={SelectedApplication.Id}");
        }

        private async Task AddInterviewAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(NewInterviewPage)}?applicationId={SelectedApplication.Id}");
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

