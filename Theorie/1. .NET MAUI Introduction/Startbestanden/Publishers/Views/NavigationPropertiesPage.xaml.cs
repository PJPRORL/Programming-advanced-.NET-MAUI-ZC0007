namespace Publishers.Views;

public partial class NavigationPropertiesPage : ContentPage
{
    public NavigationPropertiesPage(NavigationPropertiesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}