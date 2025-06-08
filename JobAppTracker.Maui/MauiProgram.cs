using JobApplicationTracker.DataAccess;
using JobApplicationTracker.DataAccess.Models;
using JobAppTracker.Maui.ViewModels;
using JobAppTracker.Maui.Services;
using JobAppTracker.Maui.Views;
using Microsoft.Extensions.Logging;

namespace JobAppTracker.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient("JobAppTrackerApiService", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7048/api/");
            });

            builder.Services.AddSingleton<INavigationHelper, NavigationHelper>();

            //Application
            builder.Services.AddSingleton<LocalApplicationStorageService>();
            builder.Services.AddTransient<ApplicationDeletionService>();
            builder.Services.AddTransient<ApplicationViewModel>();
            builder.Services.AddTransient<ApplicationsPage>();
            builder.Services.AddTransient<NewApplicationViewModel>();
            builder.Services.AddTransient<ApplicationDetailsViewModel>();
            builder.Services.AddTransient<ApplicationDetailsPage>();
            builder.Services.AddTransient<EditApplicationPage>();
            builder.Services.AddTransient<EditApplicationViewModel>();

            //Resume
            builder.Services.AddSingleton<localResumeStorageService>();
            builder.Services.AddTransient<ResumeViewModel>();
            builder.Services.AddTransient<EditResumeViewModel>();
            builder.Services.AddTransient<NewResumeViewModel>();
            builder.Services.AddTransient<ResumePage>();

            //CoverLetter
            builder.Services.AddSingleton<localCoverLetterStorageService>();
            builder.Services.AddTransient<CoverLetterViewModel>();
            builder.Services.AddTransient<EditCoverLetterViewModel>();
            builder.Services.AddTransient<NewCoverLetterViewModel>();
            builder.Services.AddTransient<CoverLetterPage>();
            
            //CheckedOnApp
            builder.Services.AddSingleton<LocalCheckedOnAppStorageService>();
            
            //Company
            builder.Services.AddSingleton<LocalCompanyStorageService>();
            builder.Services.AddTransient<CompanyDeletionService>();
            builder.Services.AddTransient<CompanyViewModel>();
            builder.Services.AddTransient<EditComapnyPage>();
            builder.Services.AddTransient<EditCompanyViewModel>();
            builder.Services.AddTransient<NewCompanyPage>();
            builder.Services.AddTransient<NewCompanyViewModel>();
            builder.Services.AddTransient<CompaniesViewPage>();
            
            //CompanyContacts
            builder.Services.AddSingleton<LocalCompanyContactStorageService>();
            builder.Services.AddTransient<NewCompanyContactViewModel>();
            builder.Services.AddTransient<NewCompanyContactPage>();
            builder.Services.AddTransient<EditCompanyContactPage>();
            builder.Services.AddTransient<EditCompanyContactViewModel>();
            //Interviews
            builder.Services.AddSingleton<LocalInterviewStorageService>();
            builder.Services.AddTransient<InterviewsViewModel>();
            builder.Services.AddTransient<NewInterviewViewModel>();
            builder.Services.AddTransient<NewInterviewPage>();
            builder.Services.AddTransient<EditInterviewViewModel>();
            builder.Services.AddTransient<EditInterviewPage>();

            //InterviewPrep
            builder.Services.AddSingleton<LocalInterviewPrepStorageService>();
            builder.Services.AddTransient<NewInterviewPrepPage>();
            builder.Services.AddTransient<NewInterviewPrepViewModel>();
            builder.Services.AddTransient<EditInterviewPrepPage>();
            builder.Services.AddTransient<EditInterviewPrepViewModel>();

            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
