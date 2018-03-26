using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Extensions;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.ViewModels.Employees;
using Catalog.Views.Employees;
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

        public ICommand AddOrdersCommand => new Command(async () => await AddOrdersCommandExecute());

        private async Task AddOrdersCommandExecute()
        {
            await _navigationService.NavigateToAsync<NewEmployeePage, NewEmployeeViewModel>(false);
        }
    }
}
