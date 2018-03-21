using System;
using Autofac;
using Catalog.ViewModels;

namespace Catalog.Infrastructure.Locators
{
    public class ViewModelLocator : IViewModelLocator
    {
        private readonly IComponentContext _componentContext;

        public ViewModelLocator(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public TViewModel Resolve<TViewModel>() where TViewModel : BaseViewModel
        {
            return _componentContext.Resolve<TViewModel>();
        }

        public TViewModel Resolve<TViewModel, TParam>(TParam param) where TViewModel : BaseViewModel
        {
            return _componentContext.Resolve<Func<TParam, TViewModel>>()(param);
        }
    }
}
