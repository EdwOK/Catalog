using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Catalog.Infrastructure.Validations;
using Catalog.Models;
using Xamarin.Forms;

namespace Catalog.ViewModels.Products
{
    public class ProductBaseViewModel : BaseViewModel
    {
        public ProductBaseViewModel()
        {
            Name = new ValidatableObject<string>();
            Description = new ValidatableObject<string>();
            Price = new ValidatableObject<double>();
            ExpirationDate = new ValidatableObject<DateTime> { Value = DateTime.UtcNow.Date };
            DeliveryDate = new ValidatableObject<DateTime> { Value = DateTime.UtcNow.Date };

            AddValidations();
        }

        protected ProductBaseViewModel(Product product) : this()
        {
            Name.Value = product.Name;
            Description.Value = product.Description;
            Price.Value = product.Price;
            ExpirationDate.Value = product.ExpirationDate;
            DeliveryDate.Value = product.DeliveryDate;
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

        public ICommand SaveProduct => new Command(async () => await SaveProductCommandExecute());

        protected virtual Task SaveProductCommandExecute()
        {
            return Task.FromResult(false);
        }

        protected override void Validate()
        {
            Name.Validate();
            Description.Validate();
            Price.Validate();
            ExpirationDate.Validate();
            DeliveryDate.Validate();
        }

        protected void AddValidations()
        {
            Name.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "название" },
                new TextRangeRule<string>(3, 100) { Name = "название" }
            });

            Description.Validations.AddRange(new IValidationRule<string>[]
            {
                new IsNotNullOrEmptyRule<string> { Name = "описание" },
                new TextRangeRule<string>(3, 200) { Name = "описание" }
            });

            Price.Validations.AddRange(new IValidationRule<double>[]
            {
                new CustomRangeRule<double>(1.0, 99999999.0) { Name = "цена" }
            });
        }

        protected override bool IsValid()
        {
            return Name.IsValid &&
                   Description.IsValid &&
                   Price.IsValid &&
                   ExpirationDate.IsValid &&
                   DeliveryDate.IsValid;
        }
    }
}
