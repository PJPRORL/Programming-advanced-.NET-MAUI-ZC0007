namespace DeMol.Views;

public partial class VraagPage : ContentPage
{
	public VraagPage(VraagPageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
    }
}