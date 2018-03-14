using Catalog.ViewModels;
using Catalog.ViewModels.Base;
using Xamarin.Forms;

namespace Catalog.Views
{
	public partial class MainPage : TabbedPage
	{
	    private MainViewModel _viewModel;

		public MainPage()
		{
			InitializeComponent();
		    BindingContext = _viewModel = ViewModelLocator.Resolve<MainViewModel>();
		}
	}
}