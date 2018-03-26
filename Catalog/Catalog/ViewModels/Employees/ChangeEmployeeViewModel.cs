using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Services.Places;

namespace Catalog.ViewModels.Employees
{
    public class ChangeEmployeeViewModel : EmployeeBaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly Employee _employee;

        public ChangeEmployeeViewModel(
            Employee employee, 
            INavigationService navigationService, 
            UnitOfWork unitOfWork, 
            IGooglePlacesService googlePlacesService,
            INetworkService networkService) 
            : base(employee, googlePlacesService, networkService)
        {
            _employee = employee;
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task SaveEmployeeCommand()
        {
            Validate();

            if (!IsValid())
            {
                return;
            }

            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _employee.FirstName = FirstName.Value;
                _employee.Surname = Surname.Value;
                _employee.LastName = LastName.Value;
                _employee.Address = Address.Value;
                _employee.PhoneNumber = PhoneNumber.Value;
                _employee.Salary = Salary.Value;
                _employee.DateOfBirth = DateOfBirth.Value;
                _employee.Position = Position.Value;

                _unitOfWork.EmployeeRepository.Update(_employee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                await _navigationService.NavigateBackToMainPageAsync();
            }
        }
    }
}
