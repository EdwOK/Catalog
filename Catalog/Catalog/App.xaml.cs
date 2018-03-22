using System.Threading.Tasks;
using Autofac;
using Catalog.DataLayer;
using Catalog.Infrastructure.Setup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Catalog.Services.Navigation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Catalog
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		    InitializeBootstrapper();
		    InitializeDatabase();
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
            var navigationService = AppSetup.Container.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
	    }

	    private void InitializeBootstrapper()
	    {
            AppSetup.Instance.Initialize();
	    }

        private void InitializeDatabase()
        {
            var dbContext = AppSetup.Container.Resolve<AppDbContext>();
            dbContext.CreateDatabase();
        }
    }
}
