namespace DeMol.Views;

public partial class ControlePage : ContentPage
{
	public ControlePage(ControleViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}