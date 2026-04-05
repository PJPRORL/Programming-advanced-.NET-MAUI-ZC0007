namespace MauiIntroductie.Views;

public partial class FotoPage : ContentPage
{
	public FotoPage(FotoViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}