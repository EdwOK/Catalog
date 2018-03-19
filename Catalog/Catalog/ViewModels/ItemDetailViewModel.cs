using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Models;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private Item _item;
        public Item Item
        {
            get => _item;
            set => Set(ref _item, value);
        }

        public ICommand SaveItem => new RelayCommand(SaveItemCommand);

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
