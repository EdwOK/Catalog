using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Navigation;
using Catalog.ViewModels.Customers;
using Catalog.ViewModels.Employees;
using Catalog.Views.Customers;
using Catalog.Views.Employees;
using Catalog.Views.Orders;
using Xamarin.Forms;

namespace Catalog.ViewModels.Orders
{
    public class OrderDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly UnitOfWork _unitOfWork;

        public OrderDetailViewModel(
            Order order,
            INavigationService navigationService, 
            UnitOfWork unitOfWork, 
            IDialogService dialogService)
        {
            Order = order;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
        }

        private Order _order;
        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                RaisePropertyChanged(() => Order);
            }
        }

        public ICommand OpenCustomerDetail => new Command(async () => await OpenCustomerDetailExecute());

        public async Task OpenCustomerDetailExecute()
        {
            if (Order == null || Order.Customer == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<CustomerDetailPage, CustomerDetailViewModel, Customer>(Order.Customer, false);
        }

        public ICommand OpenEmployeeDetail => new Command(async () => await OpenEmployeeDetailExecute());

        public async Task OpenEmployeeDetailExecute()
        {
            if (Order == null || Order.Employee == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<EmployeeDetailPage, EmployeeDetailViewModel, Employee>(Order.Employee, false);
        }


        public ICommand ChangeOrderCommand => new Command(async () => await ChangeOrderCommandExecute());

        private async Task ChangeOrderCommandExecute()
        {
            if (Order == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<NewOrderPage, ChangeOrderViewModel, Order>(Order, false);
        }

        public ICommand RemoveOrderCommand => new Command(async () => await RemoveOrderCommandExecute());

        private async Task RemoveOrderCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            bool result = await _dialogService.Confirm($"Вы подтверждаете удаление {Title}?");
            if (!result)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _unitOfWork.OrdeRepository.Remove(Order, recursive: true);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            finally
            {
                IsBusy = false;
                await _navigationService.NavigateBackToMainPageAsync();
            }
        }

        protected override void AppearingCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Order = _unitOfWork.OrdeRepository.GetById(Order.Id);
                Title = Order.Name;
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
    }
}
