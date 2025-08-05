using Microsoft.Extensions.Logging;
using MDFitJourney.ViewModels;
using MDFitJourney.Pages;
using Microcharts.Maui;

namespace MDFitJourney
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }
                );
            

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<RoutineBuilderPage>();
            builder.Services.AddSingleton<RoutineBuilderViewModel>();
            builder.Services.AddSingleton<WeightTrackerPage>();
            builder.Services.AddSingleton<WeightTrackerViewModel>();
            builder.Services.AddSingleton<InformationPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}