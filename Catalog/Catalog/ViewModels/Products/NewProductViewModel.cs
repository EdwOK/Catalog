using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Products
{
    public class NewProductViewModel : ProductBaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public NewProductViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task SaveProductCommandExecute()
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
                var product = new Product
                {
                    Name = Name.Value,
                    Description = Description.Value,
                    Price = Price.Value,
                    DeliveryDate = DeliveryDate.Value,
                    ExpirationDate = ExpirationDate.Value
                };

                _unitOfWork.ProductRepository.Add(product);
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
