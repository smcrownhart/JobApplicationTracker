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

            //Application
            builder.Services.AddSingleton<LocalApplicationStorageService>();
            builder.Services.AddTransient<ApplicationViewModel>();
            builder.Services.AddTransient<ApplicationsPage>();
            builder.Services.AddTransient<NewApplicationViewModel>();
            builder.Services.AddTransient<ApplicationDetailsViewModel>();
            builder.Services.AddTransient<ApplicationDetailsPage>();
            builder.Services.AddTransient<EditApplicationPage>();
            builder.Services.AddTransient<EditApplicationViewModel>();
            //CheckedOnApp
            builder.Services.AddSingleton<LocalCheckedOnAppStorageService>();
            builder.Services.AddTransient<CompanyViewModel>();
            //Company
            builder.Services.AddSingleton<LocalCompanyStorageService>();
            builder.Services.AddTransient<EditComapnyPage>();
            builder.Services.AddTransient<EditCompanyViewModel>();
            //CompanyContacts
            builder.Services.AddSingleton<LocalCompanyContactStorageService>();
            builder.Services.AddTransient<NewCompanyContactViewModel>();
            builder.Services.AddTransient<NewCompanyContactPage>();
            builder.Services.AddTransient<EditCompanyContactPage>();
            builder.Services.AddTransient<EditCompanyContactViewModel>();
            //Interviews
            builder.Services.AddSingleton<LocalInterviewStorageService>();
            //InterviewPrep
            builder.Services.AddSingleton<LocalInterviewPrepStorageService>();

            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
