using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Orders
{
    public class NewOrderViewModel : OrderBaseViewModel
    {
        public NewOrderViewModel(INavigationService navigationService, UnitOfWork unitOfWork) 
            : base(navigationService, unitOfWork)
        {
        }

        public NewOrderViewModel(Order order, INavigationService navigationService, UnitOfWork unitOfWork) 
            : base(order, navigationService, unitOfWork)
        {
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
                var order = new Order
                {
                    Name = Name.Value,
                    Description = Description.Value,
                    Customer = SelectedCustomer.Value,
                    Employee = SelectedEmployee.Value,
                    Products = SelectedProducts.Value.ToList()
                };

                UnitOfWork.OrdeRepository.Add(order);
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
