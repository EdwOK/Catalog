using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Models;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ItemDetailViewModel(Item item, INavigationService navigationService)
        {
            this.Item = item;
            this.Title = item.Text;
            this._navigationService = navigationService;
        }

        private Item _item;
        public Item Item
        {
            get => _item;
            set => Set(ref _item, value);
        }

        public ICommand SaveItem => new Command(async () => await SaveItemCommand());

        private async Task SaveItemCommand()
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await _navigationService.NavigateBackAsync(false);
        }
    }
}