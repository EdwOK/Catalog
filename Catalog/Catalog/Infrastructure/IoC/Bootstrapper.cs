using Autofac;

namespace Catalog.Infrastructure.IoC
{
    public class Bootstrapper
    {
        internal static IContainer Container { get; private set; }

        private static void BuildContainer(ContainerBuilder builder)
        {
            var container = builder.Build();
            container.BeginLifetimeScope();
            Container = container;
        }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CatalogModule>();
            BuildContainer(builder);
        }
    }
}
