using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Infrastructure.Locators;
using Catalog.ViewModels;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;
        private readonly IPageLocator _pageLocator;

        public NavigationService(INavigation navigation, IPageLocator pageLocator)
        {
            this._navigation = navigation;
            this._pageLocator = pageLocator;
        }

        private IReadOnlyList<Page> NavigationStack => _navigation.NavigationStack;

        public BaseViewModel PreviousPageViewModel => NavigationStack[NavigationStack.Count - 2].BindingContext as BaseViewModel;

        public Task InitializeAsync(bool animated = true)
        {
            return NavigateToAsync<MainPage, MainViewModel>(animated);
        }

        public Task NavigateToAsync<TPage, TViewModel>(bool animated = true)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel>(animated);
        }

        public Task NavigateToAsync<TPage, TViewModel>(bool animated = true, params object[] parameters)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel>(animated, parameters);
        }

        public async Task NavigateBackAsync(bool animated = true)
        {
            await _navigation.PopAsync(animated);
        }

        private async Task InternalNavigateToAsync<TPage, TViewModel>(bool animated, params object[] parameters)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            var page = await _pageLocator.Resolve<TPage, TViewModel>(parameters);
            await _navigation.PushAsync(page, animated);
        }
    }
}