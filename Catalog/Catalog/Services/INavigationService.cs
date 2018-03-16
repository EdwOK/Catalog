using System.Threading.Tasks;
using Catalog.ViewModels.Base;
using Xamarin.Forms;

namespace Catalog.Services
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync(bool animated = true);

        Task NavigateToAsync<TPage>(bool animated = true) where TPage: Page;

        Task NavigateToAsync<TPage>(bool animated = true, params object[] parameters) where TPage : Page;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
