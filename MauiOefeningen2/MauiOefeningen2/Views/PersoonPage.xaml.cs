namespace MauiOefeningen2.Views;

public partial class PersoonPage : ContentPage
{
	public PersoonPage(PersoonViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}