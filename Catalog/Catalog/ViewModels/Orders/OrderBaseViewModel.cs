using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Validations;
using Catalog.Models;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels.Orders
{
    public abstract class OrderBaseViewModel : BaseViewModel
    {
        protected UnitOfWork UnitOfWork;
        protected INavigationService NavigationService;
        protected SelectMultipleBasePage<Product> MultipleProductsBasePage;

        protected OrderBaseViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            NavigationService = navigationService;
            UnitOfWork = unitOfWork;
            
            Name = new ValidatableObject<string>();
            Description = new ValidatableObject<string>();
            SelectedCustomer = new ValidatableObject<Customer>();
            SelectedEmployee = new ValidatableObject<Employee>();
            SelectedProducts = new ValidatableObject<List<Product>>();

            AddValidations();
        }

        protected OrderBaseViewModel(Order order, INavigationService navigationService, UnitOfWork unitOfWork)
            : this(navigationService, unitOfWork)
        {
            Name.Value = order.Name;
            Description.Value = order.Description;
            SelectedCustomer.Value = order.Customer;
            SelectedEmployee.Value = order.Employee;
            SelectedProducts.Value = order.Products;
            TotalPrice = order.TotalPrice;
        }

        private ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private ValidatableObject<string> _description;
        public ValidatableObject<string> Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private ValidatableObject<Employee> _selectedEmployee;
        public ValidatableObject<Employee> SelectedEmployee
        {
            get => _selectedEmployee;
            set => Set(ref _selectedEmployee, value);
        }

        private List<Employee> _employees;
        public List<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        private ValidatableObject<Customer> _selectedCustomer;
        public ValidatableObject<Customer> SelectedCustomer
        {
            get => _selectedCustomer;
            set => Set(ref _selectedCustomer, value);
        }

        private List<Customer> _customers;
        public List<Customer> Customers
        {
            get => _customers;
            set => Set(ref _customers, value);
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }

        private ValidatableObject<List<Product>> _selectedProducts;
        public ValidatableObject<List<Product>> SelectedProducts
        {
            get => _selectedProducts;
            set => Set(ref _selectedProducts, value);
        }

        public double TotalPrice { get; set; }

        public ICommand OpenProductsSelected => new Command(async () => await OpenProductsSelectedExecute());

        protected async Task OpenProductsSelectedExecute()
        {
            if (MultipleProductsBasePage != null)
            {
                await NavigationService.NavigateToPageAsync(MultipleProductsBasePage, false);
            }
        }

        public ICommand SaveOrder => new Command(async () => await SaveOrderCommandExecute());

        protected virtual Task SaveOrderCommandExecute()
        {
            return Task.FromResult(false);
        }

        protected override void AppearingCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            if (MultipleProductsBasePage != null)
            {
                SelectedProducts.Value = MultipleProductsBasePage.GetSelection();
                TotalPrice = SelectedProducts.Value.Sum(product => product.Price);
                return;
            }

            IsBusy = true;

            try
            {
                Employees = UnitOfWork.EmployeeRepository.GetAll().ToList();
                Customers = UnitOfWork.CustomerRepository.GetAll().ToList();
                Products = UnitOfWork.ProductRepository.GetAll().Where(product => product.Order == null).ToList();
                MultipleProductsBasePage = new SelectMultipleBasePage<Product>(Products);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void Validate()
        {
            Name.Validate();
            Description.Validate();
            SelectedEmployee.Validate();
            SelectedCustomer.Validate();
            SelectedProducts.Validate();
        }

        protected void AddValidations()
        {
            Name.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "название" },
                new TextRangeRule<string>(3, 40) { Name = "название" }
            });

            Description.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "описание" },
                new TextRangeRule<string>(3, 200) { Name = "описание" }
            });

            SelectedCustomer.Validations.Add(new NotNullRule<Customer> { Name = "заказчик" });

            SelectedEmployee.Validations.Add(new NotNullRule<Employee> { Name = "работник" });

            SelectedProducts.Validations.Add(new EmptyCollectionRule<List<Product>> { Name = "товары" });
        }

        protected override bool IsValid()
        {
            return Name.IsValid && Description.IsValid &&
                   SelectedCustomer.IsValid && SelectedEmployee.IsValid &&
                   SelectedProducts.IsValid;
        }
    }
}
