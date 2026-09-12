namespace DeMol.Views
{
    public partial class MainPage : ContentPage
    {
            public MainPage(MainPageViewModel viewModel)
        {
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            InitializeComponent();
        }
    }

}
