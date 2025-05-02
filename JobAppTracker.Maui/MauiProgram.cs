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

            builder.Services.AddSingleton<MainPage>();
            //CheckedOnApp

            //Company

            //CompanyContacts

            //Interviews

            //InterviewPrep

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
