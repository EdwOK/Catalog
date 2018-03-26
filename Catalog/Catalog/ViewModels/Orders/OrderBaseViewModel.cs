using Catalog.Infrastructure.Validations;

namespace Catalog.ViewModels.Orders
{
    public abstract class OrderBaseViewModel : BaseViewModel
    {
        protected OrderBaseViewModel()
        {

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
    }
}
