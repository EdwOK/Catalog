using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ProductsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
