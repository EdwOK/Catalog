using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;
using Autofac.Extras.CommonServiceLocator;
using Catalog.Services;
using Catalog.Services.Navigation;
using Catalog.ViewModels;
using CommonServiceLocator;
using Xamarin.Forms;

namespace Catalog.Core
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<MainViewModel>();
                builder.RegisterType<AboutViewModel>();
                builder.RegisterType<ItemDetailViewModel>();
                builder.RegisterType<ItemsViewModel>();

                builder.Register(context => new NavigationProvider(Application.Current.MainPage)).As<INavigationProvider>().SingleInstance();
                builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
                builder.RegisterType<MockDataStore>().AsImplementedInterfaces();

                var container = builder.Build();
                var locator = new AutofacServiceLocator(container);
                
                ServiceLocator.SetLocatorProvider(() => locator);
            }
        }

        public static MainViewModel MainViewModel => Resolve<MainViewModel>();

        public static AboutViewModel AboutViewModel => Resolve<AboutViewModel>();

        public static ItemDetailViewModel ItemDetailViewModel => Resolve<ItemDetailViewModel>();

        public static ItemsViewModel ItemsViewModel => Resolve<ItemsViewModel>();

        public static T Resolve<T>() where T : class
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public static void Cleanup()
        {
            ServiceLocator.SetLocatorProvider(null);
        }
    }
}
