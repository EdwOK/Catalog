using Autofac;
using Catalog.Infrastructure.Locators;
using Catalog.Services;
using Catalog.Services.Navigation;
using Catalog.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace Catalog.Infrastructure.IoC
{
    public class CatalogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<AboutViewModel>();
            builder.RegisterType<ItemDetailViewModel>();
            builder.RegisterType<ItemsViewModel>();

            builder.RegisterInstance(Application.Current.MainPage).As<INavigation>();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>().SingleInstance();
            builder.RegisterType<PageLocator>().As<IPageLocator>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<MockDataStore>().AsImplementedInterfaces();
        }
    }
}
