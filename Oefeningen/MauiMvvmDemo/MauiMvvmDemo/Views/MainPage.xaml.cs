using MauiMvvmDemo.Models.ViewModels;

namespace MauiMvvmDemo.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void employeeButton1_Clicked(object sender, EventArgs e)
    {

        var employeeDetailViewModel = new EmployeeDetailViewModel
        {
            EmployeeID = "1001",
            EmployeeName = "John Thomas",
            Email = "john@email.com",
            IsPartTimer = true,
        };

        var employeeDetailPage = new EmployeeDetailPage();

        employeeDetailPage.BindingContext = employeeDetailViewModel;
        Navigation.PushAsync(employeeDetailPage);
    }

    private void employeeButton2_Clicked(object sender, EventArgs e)
    {
        var employeeDetailViewModel = new EmployeeDetailViewModel
        {
            EmployeeID = "1002",
            EmployeeName = "Peter",
            Email = "Peter@email.com",
            IsPartTimer = false,
        };

        var employeeDetailPage = new EmployeeDetailPage();

        employeeDetailPage.BindingContext = employeeDetailViewModel;
        Navigation.PushAsync(employeeDetailPage);
    }

    private void employeeButton3_Clicked(object sender, EventArgs e)
    {
        var employeeDetailViewModel = new EmployeeDetailViewModel
        {
            EmployeeID = "1003",
            EmployeeName = "Bob",
            Email = "Bob@email.com",
            IsPartTimer = true,
        };

        var employeeDetailPage = new EmployeeDetailPage();

        employeeDetailPage.BindingContext = employeeDetailViewModel;
        Navigation.PushAsync(employeeDetailPage);
    }
}