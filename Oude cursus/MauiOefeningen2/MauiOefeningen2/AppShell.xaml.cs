namespace MauiOefeningen2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(PersoonDetailPage), typeof(PersoonDetailPage));
        }
    }
}
