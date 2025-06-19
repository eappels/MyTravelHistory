using Microsoft.Extensions.Logging;
using MyTravelHistoryApp.Services;
using MyTravelHistoryApp.Services.Interfaces;
using MyTravelHistoryApp.ViewModels;
using MyTravelHistoryApp.Views;

namespace MyTravelHistoryApp
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
                })
                .UseMauiMaps();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ILocationService, LocationService>();
            builder.Services.AddSingleton<IDBService, DBService>();

            builder.Services.AddSingleton<MapViewModel>();
            builder.Services.AddTransient<MapView>();

            return builder.Build();
        }
    }
}
