namespace MauiIntroductie.Views;

public partial class StackLayoutPage : ContentPage
{
	public StackLayoutPage(StackLayoutViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}