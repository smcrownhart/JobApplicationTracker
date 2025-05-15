using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobAppTracker.Maui.Services
{
    public class ApplicationDeletionService
    {
        private readonly LocalApplicationStorageService _appService;
        private readonly LocalInterviewStorageService _interviewService;
        private readonly LocalInterviewPrepStorageService _prepService;
        private readonly LocalCompanyContactStorageService _contactService;
        private readonly LocalCheckedOnAppStorageService _checkedOnService;

        public ApplicationDeletionService(
            LocalApplicationStorageService appService,
            LocalInterviewStorageService interviewService,
            LocalInterviewPrepStorageService prepService,
            LocalCompanyContactStorageService contactService,
            LocalCheckedOnAppStorageService checkedOnService)
        {
            _appService = appService;
            _interviewService = interviewService;
            _prepService = prepService;
            _contactService = contactService;
            _checkedOnService = checkedOnService;
        }

        public async Task DeleteApplicationAndRelatedAsync(int applicationId)
        {
            var interviews = await _interviewService.LoadInterviewsAsync();
            interviews.RemoveAll(i => i.ApplicationId  == applicationId);
            await _interviewService.SaveInterviewsAsync(interviews);

            var preps = await _prepService.LoadPrepAsync();
            preps.RemoveAll(p => p.ApplicationId == applicationId);
            await _prepService.SavePrepAsync(preps);

            var contacts = await _contactService.LoadContactsAsync();
            contacts.RemoveAll(c => c.ApplicationId == applicationId);
            await _contactService.SaveContactsAsync(contacts);

            var checkedOns = await _checkedOnService.LoadCheckedOnAppsAsync();
            checkedOns.RemoveAll(c => c.ApplicationId == applicationId);
            await _checkedOnService.SaveCheckedOnAppsAsync(checkedOns);

            var applications = await _appService.LoadApplicationsAsync();
            var appToDelete = applications.FirstOrDefault(a => a.Id == applicationId);
            if (appToDelete != null)
            {
                applications.Remove(appToDelete);
                await _appService.SaveApplicationsAsync(applications);
            }
        }
    }
}
