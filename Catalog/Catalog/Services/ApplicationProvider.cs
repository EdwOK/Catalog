using Xamarin.Forms;

namespace Catalog.Services
{
    public class ApplicationProvider : IApplicationProvider
    {
        public INavigation Navigation => MainPage.Navigation;

        public Page MainPage
        {
            get => Application.Current.MainPage;
            set => Application.Current.MainPage = value;
        }
    }

    public interface IApplicationProvider
    {
        INavigation Navigation { get; }

        Page MainPage { get; set; }
    }
}
