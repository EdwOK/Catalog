using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.ViewModels.Base;
using Catalog.Views;
using Xamarin.Forms;

namespace Catalog.Services
{
    public class NavigationService : INavigationService
    {
        public BaseViewModel PreviousPageViewModel => NavigationStack[NavigationStack.Count - 2].BindingContext as BaseViewModel;

        private CustomNavigationView NavigatePage
        {
            get => Application.Current.MainPage as CustomNavigationView;
            set => Application.Current.MainPage = value;
        }

        private IReadOnlyList<Page> NavigationStack => NavigatePage?.Navigation.NavigationStack;

        public Task InitializeAsync(bool animated = true)
        {
            return NavigateToAsync<MainPage>(animated);
        }

        public Task NavigateToAsync<TPage>(bool animated = true) where TPage : Page
        {
            return InternalNavigateToAsync(typeof(TPage), animated);
        }

        public Task NavigateToAsync<TPage>(bool animated = true, params object[] parameters) where TPage : Page
        {
            return InternalNavigateToAsync(typeof(TPage), animated, parameters);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            NavigatePage?.Navigation.RemovePage(NavigationStack[NavigationStack.Count - 2]);

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            if (NavigatePage != null)
            {
                for (var i = 0; i < NavigationStack.Count - 1; i++)
                {
                    var page = NavigationStack[i];
                    NavigatePage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, bool animated, params object[] parameters)
        {
            var page = CreatePage(viewModelType, parameters);

            if (NavigatePage != null)
            {
                await NavigatePage.PushAsync(page, animated);
            }
            else
            {
                NavigatePage = new CustomNavigationView(page);
            }

            var viewModel = page.BindingContext as BaseViewModel;

            if (viewModel != null)
            {
                await viewModel.InitializeAsync(parameters);
            }
        }

        private Page CreatePage(Type pageType, params object[] parameters)
        {
            //ConstructorInfo constructor = pageType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(dc => !dc.GetParameters().Any());

            //if (!(constructor.Invoke(parameters) is Page page))
            //{
            //    throw new Exception($"Cannot locate page type for {pageType}");
            //}

            return new Page();
        }
    }
}