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
        private readonly UnitOfWork _unitOfWork;
        private readonly Employee _employee;

        public ChangeEmployeeViewModel(
            Employee employee,  
            UnitOfWork unitOfWork, 
            IGooglePlacesService googlePlacesService,
            INetworkService networkService,
            INavigationService navigationService) 
            : base(employee, googlePlacesService, networkService, navigationService)
        {
            _employee = employee;
            _unitOfWork = unitOfWork;
        }

        protected override async Task SaveEmployeeCommandExecute()
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
                await NavigationService.NavigateBackAsync(false);
            }
        }
    }
}
