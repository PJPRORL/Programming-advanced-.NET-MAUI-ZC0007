using Microsoft.Extensions.Logging;
using TussenTijdsExamen.NETMAUI.Data.Repository;
using TussenTijdsExamen.NETMAUI.Views;
using TussenTijdsExamen.NETMAUI.ViewModels;

namespace TussenTijdsExamen.NETMAUI
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
            builder.Services.AddSingleton<Views.MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<ControlePage>();
            builder.Services.AddTransient<ControleViewModel>();

            builder.Services.AddTransient<VraagPage>();
            builder.Services.AddTransient<VraagViewModel>();

            builder.Services.AddSingleton<IDocentRepository, DocentRepository>();
            builder.Services.AddSingleton<ILeerlingRepository, LeerlingRepository>();

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
