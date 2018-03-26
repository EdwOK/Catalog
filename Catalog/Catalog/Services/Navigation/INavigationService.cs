using System.Threading.Tasks;
using Catalog.ViewModels;
using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TPage, TViewModel>(bool modal, bool animated = true)
            where TPage : Page, new()
            where TViewModel : BaseViewModel;

        Task NavigateToAsync<TPage, TViewModel, TParam>(TParam parameter, bool modal, bool animated = true)
            where TPage : Page, new()
            where TViewModel : BaseViewModel;

        Task NavigateToPageAsync<TPage>(TPage page, bool modal, bool animated = true)
            where TPage : ContentPage;

        Task NavigateBackAsync(bool modal, bool animated = true);

        Task NavigateBackToMainPageAsync(bool animated = true);
    }
}
