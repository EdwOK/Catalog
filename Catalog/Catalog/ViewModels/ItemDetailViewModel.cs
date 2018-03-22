using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public ItemDetailViewModel(Item item, INavigationService navigationService, UnitOfWork unitOfWork)
        {
            Item = item;
            Title = item.Text;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
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
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _unitOfWork.TestRepository.Add(Item);
                await _navigationService.NavigateBackAsync(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}