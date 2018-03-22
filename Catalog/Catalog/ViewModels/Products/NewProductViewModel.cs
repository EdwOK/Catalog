using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.DataAccessLayer;
using Catalog.Infrastructure.Validations;
using Catalog.Models;
using Catalog.Services.Navigation;
using Xamarin.Forms;

namespace Catalog.ViewModels.Products
{
    public class NewProductViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly UnitOfWork _unitOfWork;

        public NewProductViewModel(INavigationService navigationService, UnitOfWork unitOfWork)
        {
            _navigationService = navigationService;
            _unitOfWork = unitOfWork;

            Name = new ValidatableObject<string>();
            Description = new ValidatableObject<string>();
            Price = new ValidatableObject<double>();
            ExpirationDate = new ValidatableObject<DateTime> { Value = DateTime.UtcNow.Date };
            DeliveryDate = new ValidatableObject<DateTime> { Value = DateTime.UtcNow.Date };

            AddValidations();
        }

        private ValidatableObject<string> _name;

        public ValidatableObject<string> Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private ValidatableObject<string> _description;

        public ValidatableObject<string> Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private ValidatableObject<double> _price;

        public ValidatableObject<double> Price
        {
            get => _price;
            set => Set(ref _price, value);
        }

        private ValidatableObject<DateTime> _expirationDate;

        public ValidatableObject<DateTime> ExpirationDate
        {
            get => _expirationDate;
            set => Set(ref _expirationDate, value);
        }

        private ValidatableObject<DateTime> _deliveryDate;

        public ValidatableObject<DateTime> DeliveryDate
        {
            get => _deliveryDate;
            set => Set(ref _deliveryDate, value);
        }

        public ICommand SaveProduct => new Command(async () => await SaveProductCommand());

        private async Task SaveProductCommand()
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
                var product = new Product
                {
                    Name = Name.Value,
                    Description = Description.Value,
                    Price = Price.Value,
                    DeliveryDate = DeliveryDate.Value,
                    ExpirationDate = ExpirationDate.Value
                };

                _unitOfWork.ProductRepository.Add(product);
                await _navigationService.NavigateBackAsync(false);
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

        private void Validate()
        {
            Name.Validate();
            Description.Validate();
            Price.Validate();
            ExpirationDate.Validate();
            DeliveryDate.Validate();
        }

        private bool IsValid()
        {
            return Name.IsValid && 
                   Description.IsValid && 
                   Price.IsValid && 
                   ExpirationDate.IsValid &&
                   DeliveryDate.IsValid;
        }

        private void AddValidations()
        {
            Name.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "name" },
                new TextRangeRule<string>(3, 100) { Name = "name" }
            });

            Description.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "description" },
                new TextRangeRule<string>(3, 200) { Name = "description" }
            });

            Price.Validations.AddRange(new IValidationRule<double>[]
            {
                new NumberRangeRule<double>(1.0, 99999999.0) { Name = "price" }
            });
        }
    }
}
