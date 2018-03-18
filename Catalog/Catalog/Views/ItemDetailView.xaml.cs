using Catalog.Core;
using Xamarin.Forms;
using Catalog.Models;
using Catalog.ViewModels;

namespace Catalog.Views
{
	public partial class ItemDetailPage : ContentPage
	{
	    private ItemDetailViewModel _viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this._viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            this._viewModel = ViewModelLocator.Resolve<ItemDetailViewModel>();
            this._viewModel.Item = item;

            BindingContext = this._viewModel;
        }
    }
}