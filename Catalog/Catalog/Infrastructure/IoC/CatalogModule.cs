using Autofac;
using Catalog.Infrastructure.Locators;
using Catalog.Services;
using Catalog.Services.Navigation;
using Catalog.ViewModels;

namespace Catalog.Infrastructure.IoC
{
    public class CatalogModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<AboutViewModel>().AsSelf();
            builder.RegisterType<ItemDetailViewModel>().AsSelf();
            builder.RegisterType<ItemsViewModel>().AsSelf();

            builder.RegisterType<NavigationProvider>().As<INavigationProvider>().SingleInstance();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<MockDataStore>().AsImplementedInterfaces();
        }
    }
}
