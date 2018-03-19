﻿using System.Reflection;
using Autofac;
using Catalog.Infrastructure.Locators;
using Catalog.Services;
using Catalog.Services.Navigation;
using Catalog.ViewModels;
using Module = Autofac.Module;

namespace Catalog.Infrastructure.IoC
{
    public class CatalogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<AboutViewModel>();
            builder.RegisterType<ItemDetailViewModel>();
            builder.RegisterType<ItemsViewModel>();

            builder.RegisterType<NavigationProvider>().As<INavigationProvider>().SingleInstance();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<MockDataStore>().AsImplementedInterfaces();
        }
    }
}
