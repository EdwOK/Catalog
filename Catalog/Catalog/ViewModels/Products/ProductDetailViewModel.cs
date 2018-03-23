using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels.Products
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;

        public ProductDetailViewModel(Product product, UnitOfWork unitOfWork, INavigationService navigationService, IDialogService dialogService)
        {
            Product = product;
            Title = product.Name;
            _unitOfWork = unitOfWork;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        public ICommand RemoveProductCommand => new Command(async () => await RemoveProductCommandExecute());

        private async Task RemoveProductCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            bool result = await _dialogService.Confirm($"Are you sure to remove {Title}?");
            if (!result)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _unitOfWork.ProductRepository.Remove(Product.Id);
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

        private Product _product;
        public Product Product
        {
            get => _product;
            set => Set(ref _product, value);
        }
    }
}
