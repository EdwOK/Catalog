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
        private readonly INavigationProvider _navigationProvider;

        public NavigationService(INavigationProvider navigationProvider, IViewModelLocator viewModelLocator)
        {
            this._navigationProvider = navigationProvider;
        }

        private IReadOnlyList<Page> NavigationStack => this._navigationProvider.Navigation.NavigationStack;

        public BaseViewModel PreviousPageViewModel => NavigationStack[NavigationStack.Count - 2].BindingContext as BaseViewModel;

        public Task InitializeAsync(bool animated = true)
        {
            return NavigateToAsync<MainPage, MainViewModel>(animated: animated);
        }

        public Task NavigateToAsync<TPage, TViewModel>(bool animated = true, bool modal = false)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel, System.Object>(animated, modal);
        }

        public Task NavigateToAsync<TPage, TViewModel, TParam>(bool animated = true, bool modal = false, TParam parameter = default(TParam))
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel, TParam>(animated, modal, parameter);
        }

        public async Task NavigateBackAsync(bool animated = true, bool modal = false)
        {
            if (modal)
            {
                await this._navigationProvider.Navigation.PopModalAsync(animated);
            }
            else
            {
                await this._navigationProvider.Navigation.PopAsync(animated);
            }
        }

        private async Task InternalNavigateToAsync<TPage, TViewModel, TParam>(bool animated = true, bool modal = false, TParam parameter = default(TParam))
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            var page = new TPage();

            if (page is MainPage)
            {
                this._navigationProvider.MainPage = new NavigationPage(page);
            }
            else
            {
                if (modal)
                {
                    await this._navigationProvider.Navigation.PushModalAsync(page, animated);
                }
                else
                {
                    await this._navigationProvider.Navigation.PushAsync(page, animated);
                }

                await ((TViewModel)page.BindingContext).InitializeAsync(parameter);
            }
        }
    }
}