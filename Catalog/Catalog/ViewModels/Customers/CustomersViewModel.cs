using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Extensions;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Views.Customers;
using Xamarin.Forms;

namespace Catalog.ViewModels.Customers
{
    public class CustomersViewModel : BaseViewModel
    {
        private readonly UnitOfWork _unitOfWork;

        public CustomersViewModel(
            INavigationService navigationService, 
            UnitOfWork unitOfWork)
            : base(navigationService)
        {
            _unitOfWork = unitOfWork;
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => Set(ref _customers, value);
        }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set => Set(ref _selectedCustomer, value);
        }

        protected override void AppearingCommandExecute()
        {
            LoadCustomersCommandExecute();
        }

        private void LoadCustomersCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Customers = _unitOfWork.CustomerRepository.GetAll().ToObservable();
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

        public ICommand SelectedCustomerCommand => new Command(async () => await SelectedCustomerCommandExecute());

        private async Task SelectedCustomerCommandExecute()
        {
            if (SelectedCustomer == null)
            {
                return;
            }

            await NavigationService.NavigateToAsync<CustomerDetailPage, CustomerDetailViewModel, Customer>(SelectedCustomer, false);
            SelectedCustomer = null;
        }

        public ICommand AddCustomerCommand => new Command(async () => await AddCustomerCommandExecute());

        private async Task AddCustomerCommandExecute()
        {
            await NavigationService.NavigateToAsync<NewCustomerPage, NewCustomerViewModel>(false);
        }
    }
}
