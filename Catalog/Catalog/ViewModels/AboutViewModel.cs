using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://onliner.by")));
        }

        public ICommand OpenWebCommand { get; }
    }
}