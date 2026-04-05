namespace TussenTijdsExamen.NETMAUI.Views;

using TussenTijdsExamen.NETMAUI.ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
    }
}