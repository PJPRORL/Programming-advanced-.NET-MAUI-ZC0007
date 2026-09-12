namespace MauiIntroductie.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}