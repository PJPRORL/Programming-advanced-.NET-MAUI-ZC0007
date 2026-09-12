namespace TussenTijdsExamen.NETMAUI.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
    }
}