using MauiNavigationExample.Views;

namespace MauiNavigationExample;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        // 5. Registreer routes naar pagina's die geen eigen tab of vast menu-item hebben.
        Routing.RegisterRoute(nameof(PersonenOverzichtView), typeof(PersonenOverzichtView));
    }
}
