using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;

namespace Catalog.Infrastructure.IoC
{
    public class Bootstrapper
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CatalogModule>();

            var container = builder.Build();
            var locator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}
