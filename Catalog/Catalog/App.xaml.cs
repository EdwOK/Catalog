using System.Threading.Tasks;
using Catalog.Infrastructure.IoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.Services.Navigation;
using CommonServiceLocator;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Catalog
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		    InitializeBootstrapper();
		}

		protected override async void OnStart()
		{
		    // Handle when your app start
            await InitializeNavigation();
		}

        protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

	    private Task InitializeNavigation()
	    {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            return navigationService.InitializeAsync();
	    }

	    private void InitializeBootstrapper()
	    {
            Bootstrapper.RegisterDependencies();
	    }
    }
}
