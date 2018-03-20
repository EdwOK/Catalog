namespace Catalog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(ItemsViewModel itemsViewModel, AboutViewModel aboutViewModel)
        {
            this.ItemsViewModel = itemsViewModel;
            this.AboutViewModel = aboutViewModel;
        }

        public AboutViewModel AboutViewModel { get; }

        public ItemsViewModel ItemsViewModel { get; }
    }
}
