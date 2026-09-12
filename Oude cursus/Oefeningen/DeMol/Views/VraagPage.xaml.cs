namespace DeMol.Views;

public partial class VraagPage : ContentPage
{
	public VraagPage(VraagViewModel viewModel)
	{
		BindingContext = viewModel;
	}
    protected override void OnAppearing()
    {
        InitializeComponent();
    }
}