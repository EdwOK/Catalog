using Catalog.ViewModels.Customers;
using Catalog.ViewModels.Employees;
using Catalog.ViewModels.Orders;
using Catalog.ViewModels.Products;

namespace Catalog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(
            ProductsViewModel productsViewModel, 
            AboutViewModel aboutViewModel, 
            OrdersViewModel ordersViewModel, 
            CustomersViewModel customersViewModel, 
            EmployeesViewModel employeesViewModel)
        {
            ProductsViewModel = productsViewModel;
            AboutViewModel = aboutViewModel;
            OrdersViewModel = ordersViewModel;
            CustomersViewModel = customersViewModel;
            EmployeesViewModel = employeesViewModel;
        }

        public EmployeesViewModel EmployeesViewModel { get; }

        public CustomersViewModel CustomersViewModel { get; }

        public OrdersViewModel OrdersViewModel { get; }

        public AboutViewModel AboutViewModel { get; }

        public ProductsViewModel ProductsViewModel { get; }
    }
}
