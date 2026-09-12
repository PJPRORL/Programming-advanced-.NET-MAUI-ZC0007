namespace DeMol
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(VraagPage), typeof(VraagPage));
            Routing.RegisterRoute(nameof(ControlePage), typeof(ControlePage));
        }
    }
}
