using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.ViewModels;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly INavigationProvider _navigationProvider;

        public NavigationService(INavigationProvider navigationProvider)
        {
            _navigationProvider = navigationProvider;
        }

        private IReadOnlyList<Page> NavigationStack => _navigationProvider.NavigationPage?.Navigation.NavigationStack;

        public BaseViewModel PreviousPageViewModel => NavigationStack[NavigationStack.Count - 2].BindingContext as BaseViewModel;

        public Task InitializeAsync(bool animated = true)
        {
            return NavigateToAsync<MainPage>(animated);
        }

        public Task NavigateToAsync<TPage>(bool animated = true) where TPage : Page, new()
        {
            return InternalNavigateToAsync<TPage>(animated);
        }

        public Task NavigateToAsync<TPage>(bool animated = true, params object[] parameters) where TPage : Page, new()
        {
            return InternalNavigateToAsync<TPage>(animated, parameters);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            _navigationProvider.NavigationPage?.Navigation.RemovePage(NavigationStack[NavigationStack.Count - 2]);

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            if (_navigationProvider.NavigationPage != null)
            {
                for (var i = 0; i < NavigationStack.Count - 1; i++)
                {
                    var page = NavigationStack[i];
                    _navigationProvider.NavigationPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync<TPage>(bool animated, params object[] parameters) where TPage : Page, new()
        {
            var page = new TPage();

            if (_navigationProvider.NavigationPage != null)
            {
                await _navigationProvider.NavigationPage.PushAsync(page, animated);
            }
            else
            {
                _navigationProvider.NavigationPage = new NavigationPage(page);
            }

            var viewModel = page.BindingContext as BaseViewModel;
            if (viewModel != null)
            {
                await viewModel.InitializeAsync(parameters);
            }
        }
    }
}