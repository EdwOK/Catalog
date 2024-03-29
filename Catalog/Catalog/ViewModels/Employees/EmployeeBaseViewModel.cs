﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Infrastructure.Validations;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Services.Places;
using Xamarin.Forms;

namespace Catalog.ViewModels.Employees
{
    public abstract class EmployeeBaseViewModel : BaseViewModel
    {
        protected IGooglePlacesService GooglePlacesService;
        protected INetworkService NetworkService;

        protected EmployeeBaseViewModel(
            IGooglePlacesService googlePlacesService, 
            INetworkService networkService,
            INavigationService navigationService) 
            : base(navigationService)
        {
            GooglePlacesService = googlePlacesService;
            NetworkService = networkService;

            MinDateOfBirth = DateTime.UtcNow.AddYears(-65).Date;
            MaxDateOfBirth = DateTime.UtcNow.AddYears(-18).Date;

            FirstName = new ValidatableObject<string>();
            Surname = new ValidatableObject<string>();
            LastName = new ValidatableObject<string>();
            Address = new ValidatableObject<string>();
            PhoneNumber = new ValidatableObject<string>();
            Salary = new ValidatableObject<double>();
            DateOfBirth = new ValidatableObject<DateTime> { Value = MaxDateOfBirth };
            Position = new ValidatableObject<string>();

            AddValidations();
        }

        protected EmployeeBaseViewModel(
            Employee employee, 
            IGooglePlacesService googlePlacesService, 
            INetworkService networkService,
            INavigationService navigationService) 
            : this(googlePlacesService, networkService, navigationService)
        {
            FirstName.Value = employee.FirstName;
            Surname.Value = employee.Surname;
            LastName.Value = employee.LastName;
            Address.Value = employee.Address;
            PhoneNumber.Value = employee.PhoneNumber;
            Salary.Value = employee.Salary;
            DateOfBirth.Value = employee.DateOfBirth;
            Position.Value = employee.Position;
        }

        public DateTime MinDateOfBirth { get; }

        public DateTime MaxDateOfBirth { get; }

        private ValidatableObject<string> _firstName;
        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }

        private ValidatableObject<string> _surname;
        public ValidatableObject<string> Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }

        private ValidatableObject<string> _lastName;
        public ValidatableObject<string> LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        private ValidatableObject<string> _address;
        public ValidatableObject<string> Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        private ValidatableObject<string> _phoneNumber;
        public ValidatableObject<string> PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        private ValidatableObject<double> _salary;
        public ValidatableObject<double> Salary
        {
            get => _salary;
            set => Set(ref _salary, value);
        }

        private ValidatableObject<DateTime> _dateOfBirth;
        public ValidatableObject<DateTime> DateOfBirth
        {
            get => _dateOfBirth;
            set => Set(ref _dateOfBirth, value);
        }

        private ValidatableObject<string> _position;
        public ValidatableObject<string> Position
        {
            get => _position;
            set => Set(ref _position, value);
        }

        public ICommand SaveEmployee => new Command(async () => await SaveEmployeeCommandExecute());

        protected virtual Task SaveEmployeeCommandExecute()
        {
            return Task.FromResult(false);
        }

        public ICommand SearchAddressCommand => new Command(async () => await SearchAddressCommandExecute());

        protected async Task SearchAddressCommandExecute()
        {
            if (!Address.Validate())
            {
                return;
            }

            if (!NetworkService.IsInternetConnected)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var autoCompleteRequest = new AutoCompleteRequest { Input = Address.Value };
                var autoCompletePlace = await GooglePlacesService.GetFirstAutoCompletePlace(autoCompleteRequest);

                if (autoCompletePlace != null)
                {
                    Address.Value = autoCompletePlace.Description;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override void Validate()
        {
            FirstName.Validate();
            Surname.Validate();
            LastName.Validate();
            Address.Validate();
            Position.Validate();
            PhoneNumber.Validate();
            Salary.Validate();
            DateOfBirth.Validate();
        }

        protected void AddValidations()
        {
            FirstName.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "имя" },
                new TextRangeRule<string>(3, 40) { Name = "имя" }
            });

            Surname.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "фамилия" },
                new TextRangeRule<string>(3, 40) { Name = "фамилия" }
            });

            LastName.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "отчество" },
                new TextRangeRule<string>(3, 40) { Name = "отчество" }
            });

            Address.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "адрес" },
                new TextRangeRule<string>(3, 100) { Name = "адрес" }
            });

            Position.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "должность" },
                new TextRangeRule<string>(3, 20) { Name = "должность" }
            });

            PhoneNumber.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "телефон" },
                new PhoneNumberRule<string> { Name = "телефон" }
            });

            Salary.Validations.AddRange(new IValidationRule<double>[]
            {
                new CustomRangeRule<double>(100, 99999999.0) { Name = "зарплата" }
            });
        }

        protected override bool IsValid()
        {
            return FirstName.IsValid && Surname.IsValid &&
                   LastName.IsValid && Address.IsValid &&
                   Position.IsValid && Salary.IsValid &&
                   PhoneNumber.IsValid;
        }
    }
}
