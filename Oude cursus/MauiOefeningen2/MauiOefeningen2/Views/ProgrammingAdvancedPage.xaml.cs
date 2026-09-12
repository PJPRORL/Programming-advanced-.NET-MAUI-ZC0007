using MauiOefeningen2.ViewModels;

namespace MauiOefeningen2.Views;

public partial class ProgrammingAdvancedPage : ContentPage
{
    public ProgrammingAdvancedPage(ProgrammingAdvancedViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}