using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels.Products
{
    public class ChangeProductViewModel : ProductBaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly Product _product;
        
        public ChangeProductViewModel(Product product, INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
            _product = product;

            UpdateProduct(_product);
        }

        public ICommand SaveProduct => new Command(async () => await SaveProductCommand());

        private async Task SaveProductCommand()
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
                _product.Name = Name.Value;
                _product.Description = Description.Value;
                _product.Price = Price.Value;
                _product.DeliveryDate = DeliveryDate.Value;
                _product.ExpirationDate = ExpirationDate.Value;

                _unitOfWork.ProductRepository.Update(_product);
                UpdateProduct(_product);
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
