using Catalog.Core;
using Catalog.ViewModels;
using Xamarin.Forms;

namespace Catalog.Views
{
	public partial class AboutPage : ContentPage
	{
	    private AboutViewModel _viewModel;

		public AboutPage()
		{
			InitializeComponent();

		    BindingContext = this._viewModel = ViewModelLocator.Resolve<AboutViewModel>();
		}
	}
}