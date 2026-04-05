namespace DeMol.Views;

public partial class ControlePage : ContentPage
{
	public ControlePage(ControleViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
    }
}