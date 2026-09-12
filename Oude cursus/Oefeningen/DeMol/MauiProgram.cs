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
#endif
            builder.Services.AddSingleton<IDeelnemerRepository, DeelnemerRepository>();
            builder.Services.AddSingleton<IVraagRepository, VraagRepository>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<VraagViewModel>();
            builder.Services.AddSingleton<ControleViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<VraagPage>();
            builder.Services.AddSingleton<ControlePage>();

            return builder.Build();
        }
    }
}
