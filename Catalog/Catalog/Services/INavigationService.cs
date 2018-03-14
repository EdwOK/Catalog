using System.Threading.Tasks;
using Catalog.ViewModels.Base;
using Xamarin.Forms;

namespace Catalog.Services
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TPage>() where TPage: Page;

        Task NavigateToAsync<TPage>(object parameter) where TPage : Page;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
