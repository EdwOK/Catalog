using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Orders
{
    public class ChangeOrderViewModel : OrderBaseViewModel
    {
        private readonly UnitOfWork _unitOfWork;

        public ChangeOrderViewModel(
            Order order, 
            INavigationService navigationService, 
            UnitOfWork unitOfWork) 
            : base(order, navigationService, unitOfWork)
        {
            Order = order;
            _unitOfWork = unitOfWork;
        }

        private Order _order;
        public Order Order
        {
            get => _order;
            set => Set(ref _order, value);
        }

        protected override async Task SaveOrderCommandExecute()
        {
            Validate();

            if (!IsValid())
            {
                return;
            }

            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Order.Name = Name.Value;
                Order.Description = Description.Value;
                Order.Customer = SelectedCustomer.Value;
                Order.Employee = SelectedEmployee.Value;
                Order.Products = SelectedProducts.Value;

                _unitOfWork.OrdeRepository.Update(Order);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                await NavigationService.NavigateBackAsync(false);
            }
        }
    }
}
