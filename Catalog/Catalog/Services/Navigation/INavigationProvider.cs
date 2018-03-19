using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public interface INavigationProvider
    {
        INavigation Navigation { get; }

        Page MainPage { get; set; }
    }
}
