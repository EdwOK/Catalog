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
    public class NewEmployeeViewModel : EmployeeBaseViewModel
    {
        private readonly UnitOfWork _unitOfWork;

        public NewEmployeeViewModel(
            INavigationService navigationService, 
            UnitOfWork unitOfWork, 
            IGooglePlacesService googlePlacesService, 
            INetworkService networkService) 
            : base(googlePlacesService, networkService, navigationService)
        {
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
                var employee = new Employee
                {
                    FirstName = FirstName.Value,
                    Surname = Surname.Value,
                    LastName = LastName.Value,
                    Address = Address.Value,
                    PhoneNumber = PhoneNumber.Value,
                    Position = Position.Value,
                    DateOfBirth = DateOfBirth.Value,
                    Salary = Salary.Value
                };

                _unitOfWork.EmployeeRepository.Add(employee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                await NavigationService.NavigateBackToMainPageAsync();
            }
        }
    }
}
