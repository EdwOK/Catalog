using Catalog.Core;
using Catalog.ViewModels;
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