using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Navigation;
using Catalog.Views.Products;
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

        private Product _product;
        public Product Product
        {
            get => _product;
            set => Set(ref _product, value);
        }

        public ICommand RemoveProductCommand => new Command(async () => await RemoveProductCommandExecute());

        public ICommand ChangeProductCommand => new Command(async () => await ChangeProductCommandExecute());

        private async Task ChangeProductCommandExecute()
        {
            if (Product == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<NewProductPage, ChangeProductViewModel, Product>(Product, false);
        }

        private async Task RemoveProductCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            if (Product.Order != null)
            {
                await _dialogService.Alert($"Вы не можете удалить {Title}.");
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
                _unitOfWork.ProductRepository.Remove(Product);
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
