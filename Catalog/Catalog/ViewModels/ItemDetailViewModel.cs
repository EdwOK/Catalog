using Catalog.Models;
using Catalog.ViewModels.Base;

namespace Catalog.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }

        public ItemDetailViewModel()
        {
            Title = Item?.Text;
            Item = Item;
        }

        public ItemDetailViewModel(Item item)
        {
            Item = item;
        }
    }
}
