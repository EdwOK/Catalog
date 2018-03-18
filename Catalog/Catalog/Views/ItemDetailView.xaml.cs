using Catalog.Infrastructure;
using Catalog.Infrastructure.IoC;
using Catalog.Infrastructure.Locators;
using Xamarin.Forms;
using Catalog.Models;
using Catalog.ViewModels;

namespace Catalog.Views
{
	public partial class ItemDetailPage : ContentPage
	{
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };
        }
    }
}