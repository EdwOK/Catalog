using System;
using Autofac;

namespace Catalog.Infrastructure.Setup
{
    public class AppSetup
    {
        private static readonly Lazy<AppSetup> Lazy = new Lazy<AppSetup>(() => new AppSetup());

        public static AppSetup Instance => Lazy.Value;

        public void Initialize()
        {
            Container = CreateContainer();
        }

        private AppSetup()
        {
        }

        public static IContainer Container { get; private set; }

        private IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
            return builder.Build();
        }

        private void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule<CatalogModule>();
        }
    }
}
