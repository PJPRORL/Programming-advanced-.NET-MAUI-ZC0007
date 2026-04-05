using MauiIntroductie.Views;
using MauiIntroductie.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiIntroductie
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

#if DEBUG
    		builder.Logging.AddDebug();

            // Pages en ViewModels kunnen ook per scope worden toegevoegd
#endif
            // VoorbeeldPage
            builder.Services.AddSingleton<VoorbeeldPage>();
            builder.Services.AddSingleton<PersoonViewModel>();

            // LabelsPage
            builder.Services.AddSingleton<LabelsPage>();
            builder.Services.AddSingleton<LabelsViewModel>();

            // StacklayoutPage
            builder.Services.AddSingleton<StackLayoutPage>();
            builder.Services.AddSingleton<StackLayoutViewModel>();

            // FotoPage

            builder.Services.AddSingleton<FotoPage>();
            builder.Services.AddSingleton<FotoViewModel>();

            // NamenPage

            builder.Services.AddTransient<NamenPage>();
            builder.Services.AddSingleton<NamenViewModel>();

            // WerknemerPage

            builder.Services.AddSingleton<WerknemerPage>();
            builder.Services.AddSingleton<WerknemerViewModel>();


            // Detailspage Werknemers

            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddTransient<DetailsViewModel>();

            // Interfaces en Repositories

            builder.Services.AddSingleton<IWerknemerRepository, WerknemerRepository>();
            builder.Services.AddSingleton<IFunctieRepository, FunctieRepository>();



            return builder.Build();
         }
    }
}
