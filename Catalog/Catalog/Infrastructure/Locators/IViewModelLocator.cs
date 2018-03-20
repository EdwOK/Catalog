using Catalog.ViewModels;

namespace Catalog.Infrastructure.Locators
{
    public interface IViewModelLocator
    {
        TViewModel Resolve<TViewModel, TParam>(TParam param) where TViewModel : BaseViewModel;

        TViewModel Resolve<TViewModel>() where TViewModel : BaseViewModel;
    }
}
