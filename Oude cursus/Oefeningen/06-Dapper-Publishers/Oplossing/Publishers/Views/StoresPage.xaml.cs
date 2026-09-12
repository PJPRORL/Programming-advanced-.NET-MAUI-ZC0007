using Publishers.ViewModels;

namespace Publishers.Views;

public partial class StoresPage : ContentPage
{
    public StoresPage(StoresViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}