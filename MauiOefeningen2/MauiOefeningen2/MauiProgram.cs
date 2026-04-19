using Microsoft.Extensions.Logging;

namespace MauiOefeningen2
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
            //Hoofdstuk 2: Introductie - Oefening 1: NaamTonenPage
            builder.Services.AddSingleton<NaamTonenPage>();
            builder.Services.AddSingleton<NaamTonenViewModel>();

            //Hoofdstuk 2: Introductie - Oefening 2: VakPage
            builder.Services.AddSingleton<ProgrammingAdvancedPage>();
            builder.Services.AddSingleton<ProgrammingAdvancedViewModel>();

            //Hoofdstuk 2: MVVM - Extra oefeningen - Vragenlijst
            builder.Services.AddSingleton<VragenlijstPage>();
            builder.Services.AddSingleton<VragenlijstViewModel>();

            //Hoofdstuk 2: MVVM - Alle negen
            builder.Services.AddSingleton<AlleNegenPage>();
            builder.Services.AddSingleton<AlleNegenViewModel>();

            // Hoofdstuk 3: Oefening GetallenRij
            builder.Services.AddSingleton<GetallenRijPage>();
            builder.Services.AddSingleton<GetallenRijViewModel>();

            // Hoofdstuk 3: Oefening Factuur
            builder.Services.AddSingleton<FactuurPage>();
            builder.Services.AddSingleton<FactuurViewModel>();

            // Hoofdstuk 4: Oefening Games
            builder.Services.AddSingleton<GamePage>();
            builder.Services.AddSingleton<GameViewModel>();
            builder.Services.AddSingleton<IGameRepository, GameRepository>();

            // Hoofdstuk 5: Oefening Persoon
            builder.Services.AddSingleton<PersoonPage>();
            builder.Services.AddSingleton<PersoonViewModel>();
#endif

            return builder.Build();
        }
    }
}
