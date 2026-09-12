using ExamenJanuari2026.ViewModels;

namespace ExamenJanuari2026.Views;

public partial class KlantenPage : ContentPage
{
	public KlantenPage(KlantenViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}