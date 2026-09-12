namespace TussenTijdsExamen.NETMAUI.Views;

using TussenTijdsExamen.NETMAUI.ViewModels;

public partial class VraagPage : ContentPage
{
	public VraagPage(VraagViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
