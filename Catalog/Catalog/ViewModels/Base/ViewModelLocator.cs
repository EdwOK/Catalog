using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;

namespace Catalog.ViewModels.Base
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                var builder = new ContainerBuilder();

                builder.RegisterType<MainViewModel>();
                builder.RegisterType<MainViewModel>();
                builder.RegisterType<AboutViewModel>();
                builder.RegisterType<ItemDetailViewModel>();
                builder.RegisterType<ItemsViewModel>();

                var container = builder.Build();
                var locator = new AutofacServiceLocator(container);

                ServiceLocator.SetLocatorProvider(() => locator);
            }
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public AboutViewModel AboutViewModel => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public ItemDetailViewModel ItemDetailViewModel => ServiceLocator.Current.GetInstance<ItemDetailViewModel>();

        public ItemsViewModel ItemsViewModel => ServiceLocator.Current.GetInstance<ItemsViewModel>();

        public static void Cleanup()
        {
            ServiceLocator.SetLocatorProvider(null);
        }
    }
}
