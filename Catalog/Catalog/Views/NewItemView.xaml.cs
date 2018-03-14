using System;
using Xamarin.Forms;
using Catalog.Models;
using Catalog.ViewModels;
using Catalog.ViewModels.Base;

namespace Catalog.Views
{
    public partial class NewItemPage : ContentPage
    {
        private ItemDetailViewModel _viewModel;

        public NewItemPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            _viewModel = ViewModelLocator.Resolve<ItemDetailViewModel>();
            _viewModel.Item = item;

            BindingContext = _viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", _viewModel.Item);
            await Navigation.PopModalAsync();
        }
    }
}