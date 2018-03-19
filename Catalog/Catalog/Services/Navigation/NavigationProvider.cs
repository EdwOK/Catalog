using Xamarin.Forms;

namespace Catalog.Services.Navigation
{
    public class NavigationProvider : INavigationProvider
    {
        public INavigation Navigation => MainPage.Navigation;

        public Page MainPage
        {
            get => Application.Current.MainPage;
            set => Application.Current.MainPage = value;
        }
    }
}
