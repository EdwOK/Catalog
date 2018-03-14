using System;
using System.Threading.Tasks;
using Catalog.ViewModels.Base;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.Services
{
    public class NavigationService : INavigationService
    {
        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as CustomNavigationView;
                var viewModel = mainPage?.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainPage>();
        }

        public Task NavigateToAsync<TPage>() where TPage : Page
        {
            return InternalNavigateToAsync(typeof(TPage), null);
        }

        public Task NavigateToAsync<TPage>(object parameter) where TPage : Page
        {
            return InternalNavigateToAsync(typeof(TPage), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as CustomNavigationView;
            
            mainPage?.Navigation.RemovePage(mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                for (var i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);

            if (Application.Current.MainPage is CustomNavigationView navigationPage)
            {
                await navigationPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }

            var viewModel = page.BindingContext as BaseViewModel;

            if (viewModel == null)
            {
                throw new Exception($"Cannot convert to BaseViewModel type for {nameof(viewModel)}");
            }

            await viewModel.InitializeAsync(parameter);
        }

        private Page CreatePage(Type pageType, object parameter)
        {
            Page page = Activator.CreateInstance(pageType) as Page;

            if (page == null)
            {
                throw new Exception($"Cannot locate page type for {pageType}");
            }

            return page;
        }
    }
}
