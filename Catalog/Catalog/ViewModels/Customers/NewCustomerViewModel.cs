﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Services.Places;

namespace Catalog.ViewModels.Customers
{
    public class NewCustomerViewModel : CustomerBaseViewModel
    {
        public NewCustomerViewModel(
            INavigationService navigationService, 
            UnitOfWork unitOfWork, 
            INetworkService networkService, 
            IGooglePlacesService googlePlacesService) 
            : base(networkService, navigationService, unitOfWork, googlePlacesService)
        {
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
                var customer = new Customer
                {
                    Name = Name.Value,
                    Email = Email.Value,
                    PostalCode = PostalCode.Value,
                    Address = Address.Value,
                    PhoneNumber = PhoneNumber.Value
                };

                UnitOfWork.CustomerRepository.Add(customer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                await NavigationService.NavigateBackToMainPageAsync();
            }
        }
    }
}
