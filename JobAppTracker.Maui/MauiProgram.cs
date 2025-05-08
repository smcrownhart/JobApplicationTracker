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
            builder.Services.AddSingleton<ApplicationsPage>();
            builder.Services.AddTransient<NewApplicationViewModel>();
            //CheckedOnApp
            builder.Services.AddSingleton<LocalCheckedOnAppStorageService>();
            builder.Services.AddTransient<CompanyViewModel>();
            //Company
            builder.Services.AddSingleton<LocalCompanyStorageService>();
            //CompanyContacts
            builder.Services.AddSingleton<LocalCompanyContactStorageService>();
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
