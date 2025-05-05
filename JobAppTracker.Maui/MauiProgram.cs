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
            builder.Services.AddSingleton<ApplicationViewModel>();
            builder.Services.AddTransient<EditApplicationsViewModel>();
            builder.Services.AddTransient<EditApplicationsPage>();
            builder.Services.AddSingleton<ApplicationsPage>();
            builder.Services.AddTransient<ApplicationDetailsViewModel>();
            builder.Services.AddTransient<ApplicationDetails>();
            builder.Services.AddTransient<NewApplicationViewModel>();
            builder.Services.AddTransient<NewApplicationPage>();

            //CheckedOnApp

            //Company
            builder.Services.AddSingleton<LocalCompanyStorageService>();
            builder.Services.AddTransient<CompanyViewModel>();
            builder.Services.AddTransient<NewCompanyViewModel>();
            builder.Services.AddTransient<NewCompanyPage>();
            //CompanyContacts

            //Interviews

            //InterviewPrep

            builder.Services.AddSingleton<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
