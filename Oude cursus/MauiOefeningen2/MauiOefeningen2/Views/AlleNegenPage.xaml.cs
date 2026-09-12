using MauiOefeningen2.ViewModels;

namespace MauiOefeningen2.Views;

public partial class AlleNegenPage : ContentPage
{
	public AlleNegenPage(AlleNegenViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}