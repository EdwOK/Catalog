namespace Catalog.Infrastructure.Validations
{
    public class NotNullRule<T> : IValidationRule<T>
    {
        public string ValidationMessage => $"Поле {Name} не может быть пустым.";

        public string Name { get; set; }

        public bool Check(T value)
        {
            return value != null;
        }
    }
}
