namespace TussenTijdsExamen.NETMAUI.Views;

using TussenTijdsExamen.NETMAUI.ViewModels;

public partial class ControlePage : ContentPage
{
	public ControlePage(ControleViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
