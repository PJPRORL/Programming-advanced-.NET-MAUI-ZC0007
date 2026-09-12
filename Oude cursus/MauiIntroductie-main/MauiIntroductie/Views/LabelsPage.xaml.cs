namespace MauiIntroductie.Views;

public partial class LabelsPage : ContentPage
{
	public LabelsPage(LabelsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
