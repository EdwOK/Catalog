using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Validations;
using Catalog.Models;
using Catalog.Services.Navigation;
using Catalog.Services.Networks;
using Catalog.Services.Places;
using Xamarin.Forms;

namespace Catalog.ViewModels.Customers
{
    public abstract class CustomerBaseViewModel : BaseViewModel
    {
        protected readonly UnitOfWork UnitOfWork;
        protected readonly INetworkService NetworkService;
        protected readonly IGooglePlacesService GooglePlacesService;

        protected CustomerBaseViewModel(
            INetworkService networkService, 
            INavigationService navigationService, 
            UnitOfWork unitOfWork, 
            IGooglePlacesService googlePlacesService) 
            : base(navigationService)
        {
            NetworkService = networkService;
            NavigationService = navigationService;
            UnitOfWork = unitOfWork;
            GooglePlacesService = googlePlacesService;

            Name = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();
            Address = new ValidatableObject<string>();
            PhoneNumber = new ValidatableObject<string>();
            PostalCode = new ValidatableObject<string>();

            AddValidations();
        }

        protected CustomerBaseViewModel(
            Customer customer, 
            INetworkService networkService, 
            INavigationService navigationService,
            UnitOfWork unitOfWork,
            IGooglePlacesService googlePlacesService)
            : this(networkService, navigationService, unitOfWork, googlePlacesService)
        {
            Name.Value = customer.Name;
            Address.Value = customer.Address;
            Email.Value = customer.Email;
            PhoneNumber.Value = customer.PhoneNumber;
            PostalCode.Value = customer.PostalCode;
        }

        private ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private ValidatableObject<string> _address;
        public ValidatableObject<string> Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        private ValidatableObject<string> _postalCode;
        public ValidatableObject<string> PostalCode
        {
            get => _postalCode;
            set => Set(ref _postalCode, value);
        }

        private ValidatableObject<string> _phoneNumber;
        public ValidatableObject<string> PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
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

        public ICommand SaveCustomer => new Command(async () => await SaveCustomerCommandExecute());

        protected virtual Task SaveCustomerCommandExecute()
        {
            return Task.FromResult(false);
        }

        protected override void Validate()
        {
            Name.Validate();
            Email.Validate();
            PostalCode.Validate();
            PhoneNumber.Validate();
            Address.Validate();
        }

        protected void AddValidations()
        {
            Name.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "название" },
                new TextRangeRule<string>(3, 40) { Name = "название" }
            });

            Email.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "почта" },
                new EmailRule<string> { Name = "почта" }
            });

            PostalCode.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "индекс" },
                new PostalCodeRule<string>(6) { Name = "индекс" }
            });

            Address.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "адрес" },
                new TextRangeRule<string>(3, 100) { Name = "адрес" }
            });

            PhoneNumber.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "телефон" },
                new PhoneNumberRule<string> { Name = "телефон" }
            });
        }

        protected override bool IsValid()
        {
            return Name.IsValid && Email.IsValid &&
                   PhoneNumber.IsValid && PostalCode.IsValid &&
                   Address.IsValid;
        }
    }
}
