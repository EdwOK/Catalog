using Autofac;
using Catalog.DataAccessLayer;
using Catalog.DataLayer;
using Catalog.DataLayer.SQLite;
using Catalog.Infrastructure.Locators;
using Catalog.Services;
using Catalog.Services.Dialogs;
using Catalog.Services.Locations;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Services.Places;
using Catalog.ViewModels;
using Catalog.ViewModels.Customers;
using Catalog.ViewModels.Employees;
using Catalog.ViewModels.Orders;
using Catalog.ViewModels.Products;
using Xamarin.Forms.Maps;

namespace Catalog.Infrastructure.Setup
{
    public class CatalogModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<ProductsViewModel>().AsSelf();
            builder.RegisterType<ProductDetailViewModel>().AsSelf();
            builder.RegisterType<NewProductViewModel>().AsSelf();
            builder.RegisterType<ChangeProductViewModel>().AsSelf();

            builder.RegisterType<EmployeesViewModel>().AsSelf();
            builder.RegisterType<EmployeeDetailViewModel>().AsSelf();
            builder.RegisterType<NewEmployeeViewModel>().AsSelf();
            builder.RegisterType<ChangeEmployeeViewModel>().AsSelf();

            builder.RegisterType<OrdersViewModel>().AsSelf();

            builder.RegisterType<CustomersViewModel>().AsSelf();
            builder.RegisterType<CustomerDetailViewModel>().AsSelf();
            builder.RegisterType<NewCustomerViewModel>().AsSelf();
            builder.RegisterType<ChangeCustomerViewModel>().AsSelf();

            builder.RegisterType<ApplicationProvider>().As<IApplicationProvider>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<DataLayer.SQLite.SQLite>().As<ISQLite>();
            builder.RegisterType<AppDbContext>().AsSelf();
            builder.RegisterType(typeof(UnitOfWork)).AsSelf();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<Geocoder>().AsSelf();
            builder.RegisterType<LocationService>().As<ILocationService>();
            builder.RegisterType<NetworkService>().As<INetworkService>();
            builder.RegisterType<GooglePlacesService>().As<IGooglePlacesService>();
        }
    }
}
