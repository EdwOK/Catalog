﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Catalog.DataAccessLayer;
using Catalog.Models;
using Catalog.Services.Navigation;

namespace Catalog.ViewModels.Employees
{
    public class NewEmployeeViewModel : EmployeeBaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public NewEmployeeViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
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
                await _navigationService.NavigateBackAsync(false);
            }
        }
    }
}
