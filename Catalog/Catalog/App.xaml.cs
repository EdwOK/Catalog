using System.Threading.Tasks;
using Catalog.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.Services;
using Catalog.Services.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Catalog
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
        }

		protected override async void OnStart()
		{
            await InitNavigation();
        }

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

	    private Task InitNavigation()
	    {
	        var navigationService = ViewModelLocator.Resolve<INavigationService>();
	        return navigationService.InitializeAsync();
	    }
    }
}
