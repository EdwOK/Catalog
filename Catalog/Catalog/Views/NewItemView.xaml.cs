using System;
using Xamarin.Forms;
using Catalog.Models;
using Catalog.ViewModels;

namespace Catalog.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", _viewModel.Item);
            await Navigation.PopModalAsync();
        }
    }
}