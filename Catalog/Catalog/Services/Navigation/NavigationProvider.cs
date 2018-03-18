using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public class NavigationProvider : INavigationProvider
    {
        public NavigationProvider(Page mainPage)
        {
            this.NavigationPage = mainPage as NavigationPage;
        }

        public NavigationPage NavigationPage { get; set; }

        public INavigation Navigation => NavigationPage.Navigation;
    }
}
