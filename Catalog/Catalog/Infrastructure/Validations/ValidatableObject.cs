using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace Catalog.Infrastructure.Validations
{
    public class ValidatableObject<T> : ObservableObject, IValidity
    {
        private List<string> _errors;
        private bool _isValid;
        private T _value;

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public List<IValidationRule<T>> Validations { get; }

        public List<string> Errors
        {
            get => _errors;
            set => Set(ref _errors, value);
        }

        public T Value
        {
            get => _value;
            set => Set(ref _value, value);
        }

        public bool IsValid
        {
            get => _isValid;
            set => Set(ref _isValid, value);
        }

        public bool Validate()
        {
            Errors.Clear();

            var errors = Validations.Where(v => !v.Check(Value)).Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }
    }
}