using Catalog.ViewModels;

namespace Catalog.Infrastructure.Locators
{
    public interface IViewModelLocator
    {
        TViewModel Resolve<TViewModel>() where TViewModel : BaseViewModel;
    }
}
