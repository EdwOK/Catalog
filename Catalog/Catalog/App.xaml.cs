using System.Threading.Tasks;
using Catalog.ViewModels.Base;
using Catalog.Views;
using GalaSoft.MvvmLight.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using INavigationService = Catalog.Services.INavigationService;

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
            base.OnStart();

            await InitNavigation();

            base.OnResume();
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
