using System.Collections;

namespace Catalog.Infrastructure.Validations
{
    public class EmptyCollectionRule<T> : IValidationRule<T>
        where T: ICollection
    {
        public string ValidationMessage => $"Коллекция {Name} не может быть пустая.";

        public string Name { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            return value.Count > 0;
        }
    }
}
