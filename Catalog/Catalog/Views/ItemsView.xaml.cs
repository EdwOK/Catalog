using System;
using Xamarin.Forms;
using Catalog.Models;
using Catalog.ViewModels;
using Catalog.ViewModels.Base;

namespace Catalog.Views
{
	public partial class ItemsPage : ContentPage
	{
	    private ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = this._viewModel = ViewModelLocator.ItemsViewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
            {
                return;
            }

            var viewModel = ViewModelLocator.Resolve<ItemDetailViewModel>();
            viewModel.Item = item;

            await Navigation.PushAsync(new ItemDetailPage(viewModel));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

	    async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this._viewModel.Items.Count == 0)
            {
                this._viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}