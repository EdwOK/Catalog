using System;
using System.Threading.Tasks;
using Catalog.Infrastructure.Locators;
using Catalog.ViewModels;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IApplicationProvider _applicationProvider;
        private readonly IViewModelLocator _viewModelLocator;

        public NavigationService(IApplicationProvider applicationProvider, IViewModelLocator viewModelLocator)
        {
            _applicationProvider = applicationProvider;
            _viewModelLocator = viewModelLocator;
        }

        public BaseViewModel PreviousPageViewModel
        {
            get
            {
                var navigationStack = _applicationProvider.Navigation.NavigationStack;
                return navigationStack[navigationStack.Count - 1].BindingContext as BaseViewModel;
            }
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainPage, MainViewModel>(false);
        }

        public Task NavigateToAsync<TPage, TViewModel>(bool modal, bool animated = true)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel>(modal, animated);
        }

        public Task NavigateToAsync<TPage, TViewModel, TParam>(TParam parameter, bool modal, bool animated = true)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync<TPage, TViewModel, TParam>(parameter, animated, modal);
        }

        public async Task NavigateBackAsync(bool modal, bool animated = true)
        {
            if (modal)
            {
                await _applicationProvider.Navigation.PopModalAsync(animated);
            }
            else
            {
                await _applicationProvider.Navigation.PopAsync(animated);
            }

            PreviousPageViewModel?.Dispose();
        }

        private async Task InternalNavigateToAsync<TPage, TViewModel, TParam>(TParam parameter, bool animated, bool modal)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            var viewModel = _viewModelLocator.Resolve<TViewModel, TParam>(parameter);
            await CanNavigate<TPage, TViewModel>(viewModel, animated, modal);
        }

        private async Task InternalNavigateToAsync<TPage, TViewModel>(bool animated, bool modal)
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            var viewModel = _viewModelLocator.Resolve<TViewModel>();
            await CanNavigate<TPage, TViewModel>(viewModel, animated, modal);
        }

        private async Task CanNavigate<TPage, TViewModel>(TViewModel viewModel, bool animated, bool modal) 
            where TPage : Page, new()
            where TViewModel : BaseViewModel
        {
            var page = new TPage
            {
                BindingContext = viewModel
            };

            if (page is MainPage)
            {
                _applicationProvider.MainPage = new NavigationPage(page);
            }
            else
            {
                if (modal)
                {
                    await _applicationProvider.Navigation.PushModalAsync(page, animated);
                }
                else
                {
                    await _applicationProvider.Navigation.PushAsync(page, animated);
                }
            }
        }
    }
}