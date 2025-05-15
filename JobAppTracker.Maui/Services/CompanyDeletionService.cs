using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobAppTracker.Maui.Services
{
    public class CompanyDeletionService
    {
        private readonly LocalCompanyStorageService _companyService;
        private readonly LocalApplicationStorageService _appService;
        
        public CompanyDeletionService(LocalCompanyStorageService companyService, LocalApplicationStorageService appService)
        {
            _companyService = companyService;
            _appService = appService;
        }

        public async Task<bool> CanDeleteCompanyAsync(int companyId)
        {
            var applications = await _appService.LoadApplicationsAsync();
            return !applications.Any(a => a.CompanyId == companyId);
        }

        public async Task<bool> DeleteCompanyAsync(int companyId) 
        { 
            var canDelete = await CanDeleteCompanyAsync(companyId);

            if (!canDelete)
            {
                await Shell.Current.DisplayAlert("You must first delete all applications associated with this company.", "Error", "OK");
                return false;
            }

            await _companyService.DeleteCompanyAsync(companyId);
            return true;

        }
        
    }
}
