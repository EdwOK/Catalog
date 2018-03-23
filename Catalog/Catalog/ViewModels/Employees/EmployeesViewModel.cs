using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Extensions;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Views.Employees;
using Xamarin.Forms;

namespace Catalog.ViewModels.Employees
{
    public class EmployeesViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public EmployeesViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set => Set(ref _selectedEmployee, value);
        }

        public override void AppearingCommandExecute()
        {
            LoadEmployeesCommandExecute();
        }

        private void LoadEmployeesCommandExecute()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Employees = _unitOfWork.EmployeeRepository.GetAll().ToObservable();
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

        public ICommand SelectedEmployeeCommand => new Command(async () => await SelectedEmployeeCommandExecute());

        private async Task SelectedEmployeeCommandExecute()
        {
            if (SelectedEmployee == null)
            {
                return;
            }

            await _navigationService.NavigateToAsync<NewEmployeePage, EmployeeDetailViewModel, Employee>(SelectedEmployee, false);
            SelectedEmployee = null;
        }

        public ICommand AddEmployeeCommand => new Command(async () => await AddEmployeeCommandExecute());

        private async Task AddEmployeeCommandExecute()
        {
            await _navigationService.NavigateToAsync<NewEmployeePage, NewEmployeeViewModel>(false);
        }
    }
}
