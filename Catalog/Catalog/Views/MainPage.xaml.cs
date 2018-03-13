using Catalog.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Catalog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MvxTabbedPage<MainViewModel>
	{
		public MainPage ()
		{
			InitializeComponent ();
		}
    }
}