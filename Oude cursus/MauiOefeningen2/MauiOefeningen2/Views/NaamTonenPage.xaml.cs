using MauiOefeningen2.ViewModels;

namespace MauiOefeningen2.Views;

public partial class NaamTonenPage : ContentPage
{
    public NaamTonenPage(NaamTonenViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
    }
}