using Autofac;

namespace Catalog.Infrastructure.IoC
{
    public class Bootstrapper
    {
        internal static IContainer Container { get; private set; }

        private static void BuildContainer(ContainerBuilder builder)
        {
            Container = builder.Build();
        }

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CatalogModule>();
            BuildContainer(builder);
        }
    }
}
