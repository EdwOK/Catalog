using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public interface INavigationProvider
    {
        NavigationPage NavigationPage { get; set; }

        INavigation Navigation { get; }
    }
}
