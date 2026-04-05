namespace MauiIntroductie.Views;

public partial class NamenPage : ContentPage
{
	public NamenPage(NamenViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}