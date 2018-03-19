﻿using System.Threading.Tasks;
using Catalog.ViewModels;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Locators
{
    public interface IPageLocator
    {
        Task<TPage> Resolve<TPage, TViewModel, TParam>(TParam parameter)
            where TPage : Page, new()
            where TViewModel : BaseViewModel;
    }
}
