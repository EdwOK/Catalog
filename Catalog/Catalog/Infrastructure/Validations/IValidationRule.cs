namespace Catalog.Infrastructure.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; }

        string Name { get; set; }

        bool Check(T value);
    }
}
