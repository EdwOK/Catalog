using System.Threading.Tasks;
using Catalog.ViewModels;
using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync(bool animated = true);

        Task NavigateToAsync<TPage, TViewModel>(bool animated = true, bool modal = false)
            where TPage : Page, new()
            where TViewModel : BaseViewModel;

        Task NavigateToAsync<TPage, TViewModel, TParam>(bool animated = true, bool modal = false, TParam parameter = default(TParam))
            where TPage : Page, new()
            where TViewModel : BaseViewModel;

        Task NavigateBackAsync(bool animated = true, bool modal = false);
    }
}
