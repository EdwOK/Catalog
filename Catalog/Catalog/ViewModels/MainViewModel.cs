using Catalog.ViewModels.Products;

namespace Catalog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(ItemsViewModel itemsViewModel, ProductsViewModel productsViewModel, AboutViewModel aboutViewModel)
        {
            ItemsViewModel = itemsViewModel;
            ProductsViewModel = productsViewModel;
            AboutViewModel = aboutViewModel;
        }

        public AboutViewModel AboutViewModel { get; }

        public ItemsViewModel ItemsViewModel { get; }

        public ProductsViewModel ProductsViewModel { get; }
    }
}
