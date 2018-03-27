using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Services.Places;

namespace Catalog.ViewModels.Customers
{
    public class ChangeCustomerViewModel : CustomerBaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly Customer _customer;

        public ChangeCustomerViewModel(
            Customer customer,
            INetworkService networkService, 
            INavigationService navigationService, 
            UnitOfWork unitOfWork, 
            IGooglePlacesService googlePlacesService) 
            : base(customer, networkService, navigationService, unitOfWork, googlePlacesService)
        {
            _customer = customer;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task SaveCustomerCommandExecute()
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
                _customer.Name = Name.Value;
                _customer.Address = Address.Value;
                _customer.Email = Email.Value;
                _customer.PhoneNumber = PhoneNumber.Value;
                _customer.PostalCode = PostalCode.Value;

                _unitOfWork.CustomerRepository.Update(_customer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                await _navigationService.NavigateBackAsync(false);
            }
        }
    }
}
