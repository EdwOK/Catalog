using System;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public RelayCommand OpenWebCommand => new RelayCommand(() => Device.OpenUri(new Uri("https://onliner.by")));

        public AboutViewModel()
        {
            Title = "About";
        }
    }
}