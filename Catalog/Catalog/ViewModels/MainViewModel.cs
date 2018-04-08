using Catalog.Services.Navigation;
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
            OrdersViewModel ordersViewModel, 
            CustomersViewModel customersViewModel, 
            EmployeesViewModel employeesViewModel,
            INavigationService navigationService) 
            : base(navigationService)
        {
            ProductsViewModel = productsViewModel;
            OrdersViewModel = ordersViewModel;
            CustomersViewModel = customersViewModel;
            EmployeesViewModel = employeesViewModel;
        }

        public EmployeesViewModel EmployeesViewModel { get; }

        public CustomersViewModel CustomersViewModel { get; }

        public OrdersViewModel OrdersViewModel { get; }

        public ProductsViewModel ProductsViewModel { get; }
    }
}
