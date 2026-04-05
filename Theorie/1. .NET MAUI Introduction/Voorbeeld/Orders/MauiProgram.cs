using Microsoft.Extensions.Logging;

namespace Orders
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
            builder.Services.AddTransient<WerknemersPage>();
            builder.Services.AddTransient<OrdersPage>();
            builder.Services.AddTransient<OrdersExtendedPage>();

            builder.Services.AddTransient<WerknemersViewModel>();
            builder.Services.AddTransient<OrdersViewModel>();
            builder.Services.AddTransient<OrdersExtendedViewModel>();

            builder.Services.AddSingleton<WerknemerRepository>();
            builder.Services.AddSingleton<OrderRepository>();
            builder.Services.AddSingleton<KlantRepository>();

            return builder.Build();
        }
    }
}