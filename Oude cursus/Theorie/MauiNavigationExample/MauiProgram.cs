using Microsoft.Extensions.Logging;
using MauiNavigationExample.Views;
using MauiNavigationExample.ViewModels;

namespace MauiNavigationExample;

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

        // Views
        builder.Services.AddTransient<PersonenBeheerView>();
        builder.Services.AddTransient<PersonenOverzichtView>();

        // ViewModels
        builder.Services.AddTransient<PersonenBeheerViewModel>();
        builder.Services.AddTransient<PersonenOverzichtViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
