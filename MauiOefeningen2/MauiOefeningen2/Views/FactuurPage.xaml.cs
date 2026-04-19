using MauiOefeningen2.ViewModels;

namespace MauiOefeningen2.Views;

public partial class FactuurPage : ContentPage
{
	public FactuurPage(FactuurViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}