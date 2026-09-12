using Publishers.ViewModels;

namespace Publishers.Views;

public partial class EmployeesPage : ContentPage
{
    public EmployeesPage(EmployeesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}