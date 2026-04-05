namespace MauiIntroductie.Views;

public partial class WerknemerPage : ContentPage
{
	public WerknemerPage(WerknemerViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
    }
}