using Autofac;
using Catalog.DataAccessLayer;
using Catalog.DataLayer;
using Catalog.DataLayer.SQLite;
using Catalog.Infrastructure.Locators;
using Catalog.Services;
using Catalog.Services.Dialogs;
using Catalog.Services.Navigation;
using Catalog.ViewModels;

namespace Catalog.Infrastructure.Setup
{
    public class CatalogModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<AboutViewModel>().AsSelf();
            builder.RegisterType<ItemDetailViewModel>().AsSelf();
            builder.RegisterType<ItemsViewModel>().AsSelf();

            builder.RegisterType<ApplicationProvider>().As<IApplicationProvider>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<DataLayer.SQLite.SQLite>().As<ISQLite>();
            builder.RegisterType<AppDbContext>().AsSelf();
            builder.RegisterType(typeof(UnitOfWork)).AsSelf();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
        }
    }
}
