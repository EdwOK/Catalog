using System.Collections.ObjectModel;
using Catalog.BusinessLayer.Entities;
using Catalog.DataAccessLayer;
using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Products
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public ProductsViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;

            Products = new ObservableCollection<Product>();
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value);
        }
    }
}
