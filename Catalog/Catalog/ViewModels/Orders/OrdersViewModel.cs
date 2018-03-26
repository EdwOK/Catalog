using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Extensions;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Views.Orders;
using Xamarin.Forms;

namespace Catalog.ViewModels.Orders
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public OrdersViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => Set(ref _orders, value);
        }

        protected override void AppearingCommandExecute()
        {
            LoadEmployeesCommandExecute();
        }

        private void LoadEmployeesCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Orders = _unitOfWork.OrdeRepository.GetAll().ToObservable();
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

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }

        public ICommand SelectedOrderCommand => new Command(async () => await SelectedOrderCommandExecute());

        private async Task SelectedOrderCommandExecute()
        {
            if (SelectedOrder == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<OrderDetailPage, OrderDetailViewModel, Order>(SelectedOrder, false);
            SelectedOrder = null;
        }

        public ICommand AddOrderCommand => new Command(async () => await AddOrderCommandExecute());

        private async Task AddOrderCommandExecute()
        {
            await _navigationService.NavigateToAsync<NewOrderPage, NewOrderViewModel>(false);
        }
    }
}
