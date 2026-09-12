using Microsoft.Extensions.Logging;

namespace DeMol
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

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ControlePage>();
            builder.Services.AddSingleton<ControleViewModel>();
            builder.Services.AddSingleton<VraagPage>();
            builder.Services.AddSingleton<VraagPageViewModel>();

            builder.Services.AddSingleton<IVraagRepository, VraagRepository>();
            builder.Services.AddSingleton<IDeelnemerRepository, DeelnemerRepository>();
#endif

            return builder.Build();
        }
    }
}
