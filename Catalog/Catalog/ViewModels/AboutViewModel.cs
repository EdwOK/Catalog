using System;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new MvxCommand(() => Device.OpenUri(new Uri("https://onliner.by")));
        }

        public IMvxCommand OpenWebCommand { get; }
    }
}