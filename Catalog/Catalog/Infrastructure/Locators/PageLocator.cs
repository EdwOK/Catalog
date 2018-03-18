using System.Threading.Tasks;
using Catalog.ViewModels;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Locators
{
    public class PageLocator : IPageLocator
    {
        private readonly IViewModelLocator _viewModelLocator;

        public PageLocator(IViewModelLocator viewModelLocator)
        {
            this._viewModelLocator = viewModelLocator;
        }

        public async Task<TPage> Resolve<TPage, TViewModel>(params object[] parameters)
            where TPage : Page, new() 
            where TViewModel : BaseViewModel
        {
            var viewModel = _viewModelLocator.Resolve<TViewModel>();
            await viewModel.InitializeAsync(parameters);

            var page = new TPage
            {
                BindingContext = viewModel
            };

            page.Appearing += (sender, args) => viewModel.OnAppearing();
            page.Disappearing += (sender, args) => viewModel.OnDisappearing();

            return page;
        }
    }
}
