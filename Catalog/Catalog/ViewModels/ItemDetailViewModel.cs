using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public ICommand SaveItem { get; set; }

        public ItemDetailViewModel()
        {
            SaveItem = new RelayCommand(SaveItemCommand);
        }

        private void SaveItemCommand()
        {
            MessagingCenter.Send(this, "AddItem", Item);
        }

        public override Task InitializeAsync<TParam>(TParam parameter)
        {
            var item = parameter as Item;

            if (item != null)
            {
                this.Item = item;
                this.Title = item.Text;
            }

            return Task.FromResult(true);
        }
    }
}
