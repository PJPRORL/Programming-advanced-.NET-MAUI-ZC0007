using MauiNavigationExample.ViewModels;

namespace MauiNavigationExample.Views;

public partial class PersonenOverzichtView : ContentPage
{
    public PersonenOverzichtView(PersonenOverzichtViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
