using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Navigation;
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
            Title = order.Name;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
        }

        private Order _order;
        public Order Order
        {
            get => _order;
            set => Set(ref _order, value);
        }

        public ICommand ChangeOrderCommand => new Command(async () => await ChangeOrderCommandExecute());

        private Task ChangeOrderCommandExecute()
        {
            return Task.CompletedTask;
            // await _navigationService.NavigateToAsync<NewCustomerPage, ChangeO, Order>(Order, false);
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
                await _navigationService.NavigateBackAsync(false);
            }
        }
    }
}
