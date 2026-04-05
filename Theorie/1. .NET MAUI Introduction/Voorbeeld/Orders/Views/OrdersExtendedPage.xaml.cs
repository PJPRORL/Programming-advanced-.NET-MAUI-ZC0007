
namespace Orders.Views;

public partial class OrdersExtendedPage : ContentPage
{
    public OrdersExtendedPage(OrdersExtendedViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}