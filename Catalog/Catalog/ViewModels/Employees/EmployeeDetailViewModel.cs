using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Dialogs;
using Catalog.Services.Navigation;
using Catalog.Views.Employees;
using Xamarin.Forms;

namespace Catalog.ViewModels.Employees
{
    public class EmployeeDetailViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;

        public EmployeeDetailViewModel(Employee employee, UnitOfWork unitOfWork, INavigationService navigationService, IDialogService dialogService)
        {
            Employee = employee;
            Title = employee.FullName;
            _unitOfWork = unitOfWork;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        private Employee _employee;
        public Employee Employee
        {
            get => _employee;
            set => Set(ref _employee, value);
        }

        public ICommand ChangeEmployeeCommand => new Command(async () => await ChangeEmployeeCommandExecute());

        private async Task ChangeEmployeeCommandExecute()
        {
            if (Employee == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<NewEmployeePage, ChangeEmployeeViewModel, Employee>(Employee, false);
        }

        public ICommand RemoveEmployeeCommand => new Command(async () => await RemoveEmployeeCommandExecute());

        private async Task RemoveEmployeeCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            bool result = await _dialogService.Confirm($"Вы подтверждаете удаление {Title}?");
            if (!result)
            {
                return;
            }

            IsBusy = true;

            try
            {
                _unitOfWork.EmployeeRepository.Remove(Employee.Id);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            finally
            {
                IsBusy = false;
                await _navigationService.NavigateBackAsync(false);
            }
        }
    }
}
