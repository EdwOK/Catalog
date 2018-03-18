using Catalog.ViewModels;
using CommonServiceLocator;

namespace Catalog.Infrastructure.Locators
{
    public class ViewModelLocator : IViewModelLocator
    {
        public MainViewModel MainViewModel => Resolve<MainViewModel>();

        public AboutViewModel AboutViewModel => Resolve<AboutViewModel>();

        public ItemDetailViewModel ItemDetailViewModel => Resolve<ItemDetailViewModel>();

        public ItemsViewModel ItemsViewModel => Resolve<ItemsViewModel>();

        public TViewModel Resolve<TViewModel>() where TViewModel : BaseViewModel
        {
            return ServiceLocator.Current.GetInstance<TViewModel>();
        }
    }
}
