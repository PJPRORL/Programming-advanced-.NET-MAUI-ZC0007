using MauiNavigationExample.ViewModels;

namespace MauiNavigationExample.Views;

public partial class PersonenBeheerView : ContentPage
{
    public PersonenBeheerView(PersonenBeheerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
