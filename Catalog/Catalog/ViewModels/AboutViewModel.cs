using System;
using Catalog.ViewModels.Base;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public RelayCommand OpenWebCommand { get; set; }

        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new RelayCommand(() => Device.OpenUri(new Uri("https://onliner.by")));
        }
    }
}