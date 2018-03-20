using System;
using Autofac;
using Autofac.Features.OwnedInstances;
using Catalog.ViewModels;

namespace Catalog.Infrastructure.Locators
{
    public class ViewModelLocator : IViewModelLocator, IDisposable
    {
        private readonly ILifetimeScope _lifetimeScope;

        public ViewModelLocator(ILifetimeScope lifetimeScope)
        {
            this._lifetimeScope = lifetimeScope;
        }

        public TViewModel Resolve<TViewModel>() where TViewModel : BaseViewModel
        {
            return this._lifetimeScope.Resolve<Owned<TViewModel>>().Value;
        }

        public TViewModel Resolve<TViewModel, TParam>(TParam param) where TViewModel : BaseViewModel
        {
            return this._lifetimeScope.Resolve<Func<TParam, Owned<TViewModel>>>()(param).Value;
        }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }
    }
}
