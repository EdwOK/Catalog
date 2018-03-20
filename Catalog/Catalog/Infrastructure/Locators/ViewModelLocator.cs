using System;
using Autofac;
using Catalog.ViewModels;

namespace Catalog.Infrastructure.Locators
{
    public class ViewModelLocator : IViewModelLocator
    {
        private readonly ILifetimeScope _lifetimeScope;

        public ViewModelLocator(ILifetimeScope lifetimeScope)
        {
            this._lifetimeScope = lifetimeScope;
        }

        public TViewModel Resolve<TViewModel>() where TViewModel : BaseViewModel
        {
            return this._lifetimeScope.Resolve<TViewModel>();
        }

        public TViewModel Resolve<TViewModel, TParam>(TParam param) where TViewModel : BaseViewModel
        {
            return this._lifetimeScope.Resolve<Func<TParam, TViewModel>>()(param);
        }
    }
}
