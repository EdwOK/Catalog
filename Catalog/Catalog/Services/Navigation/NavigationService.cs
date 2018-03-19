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

        public Task NavigateToAsync<TPage, TViewModel>(bool modal = false, bool animated = true)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel, System.Object>(modal, animated);
        }

        public Task NavigateToAsync<TPage, TViewModel, TParam>(bool modal = false, bool animated = true, TParam parameter = default(TParam))
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel, TParam>(modal, animated, parameter);
        }

        public async Task NavigateBackAsync(bool modal = false, bool animated = true)
        {
            if (modal)
            {
                await _navigation.PopModalAsync(animated);
            }
            else
            {
                await _navigation.PopAsync(animated);
            }
        }

        private async Task InternalNavigateToAsync<TPage, TViewModel, TParam>(bool modal = false, bool animated = true, TParam parameter = default(TParam))
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            var page = await _pageLocator.Resolve<TPage, TViewModel, TParam>(parameter);

            if (modal)
            {
                await _navigation.PushModalAsync(page, animated);
            }
            else
            {
                await _navigation.PushAsync(page, animated);
            }
        }
    }
}