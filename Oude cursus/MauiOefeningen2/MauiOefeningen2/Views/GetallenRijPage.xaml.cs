using MauiOefeningen2.ViewModels;

namespace MauiOefeningen2.Views;

public partial class GetallenRijPage : ContentPage
{
	public GetallenRijPage(GetallenRijViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}