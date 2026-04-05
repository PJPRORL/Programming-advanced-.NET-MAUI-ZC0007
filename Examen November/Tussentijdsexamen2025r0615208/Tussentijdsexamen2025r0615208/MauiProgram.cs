using Microsoft.Extensions.Logging;
using Tussentijdsexamen2025r0615208.Data.IRepositories;
using Tussentijdsexamen2025r0615208.Data.Repositories;

/*Jeroen Piussi, r0615208*/

namespace Tussentijdsexamen2025r0615208
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
            builder.Services.AddSingleton<IDocenten, DocentRepository>();
            builder.Services.AddSingleton<IStudenten, StudentRepository>();
#endif

            return builder.Build();
        }
    }
}
