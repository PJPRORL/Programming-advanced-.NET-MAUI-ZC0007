using MauiOefeningen2.ViewModels;

namespace MauiOefeningen2.Views;

public partial class VragenlijstPage : ContentPage
{
	public VragenlijstPage(VragenlijstViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}