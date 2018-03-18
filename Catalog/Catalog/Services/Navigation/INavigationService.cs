using System.Threading.Tasks;
using Catalog.ViewModels;
using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync(bool animated = true);

        Task NavigateToAsync<TPage>(bool animated = true) where TPage: Page, new();

        Task NavigateToAsync<TPage>(bool animated = true, params object[] parameters) where TPage : Page, new();

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
