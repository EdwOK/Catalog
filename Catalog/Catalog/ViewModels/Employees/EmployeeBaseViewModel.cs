using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Infrastructure.Validations;
using Catalog.Models;
using Catalog.Services.Networks;
using Catalog.Services.Places;
using Xamarin.Forms;

namespace Catalog.ViewModels.Employees
{
    public class EmployeeBaseViewModel : BaseViewModel
    {
        protected IGooglePlacesService GooglePlacesService;
        protected INetworkService NetworkService;

        public EmployeeBaseViewModel(IGooglePlacesService googlePlacesService, INetworkService networkService)
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

        protected EmployeeBaseViewModel(Employee employee, IGooglePlacesService googlePlacesService, INetworkService networkService) 
            : this(googlePlacesService, networkService)
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

        public ICommand SaveEmployee => new Command(async () => await SaveEmployeeCommand());

        protected virtual Task SaveEmployeeCommand()
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

            var autoCompleteRequest = new AutoCompleteRequest { Input = Address.Value };
            var autoCompleteResult = await GooglePlacesService.GetAutoCompletePlaces(autoCompleteRequest);

            var predictions = autoCompleteResult.Predictions;

            if (predictions.Count > 0)
            {
                var autoCompleteAddress = predictions.FirstOrDefault();

                Address.Value = autoCompleteAddress.Description;
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
                new PhoneNumberRule<string>() { Name = "телефон" }
            });

            Salary.Validations.AddRange(new IValidationRule<double>[]
            {
                new CustomRangeRule<double>(1.0, 99999999.0) { Name = "зарплата" }
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
