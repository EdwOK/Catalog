using Catalog.DataAccessLayer;
using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Employees
{
    public class NewEmployeeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public NewEmployeeViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }
    }
}
