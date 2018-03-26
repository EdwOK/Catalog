using System.Text.RegularExpressions;

namespace Catalog.Infrastructure.Validations
{
    public class PostalCodeRule<T> : IValidationRule<T>
    {
        public string ValidationMessage => $"Поле {Name} должно содержать {Numbers} цифр.";

        public string Name { get; set; }

        public int Numbers { get; set; }

        public PostalCodeRule(int numbers)
        {
            Numbers = numbers;
        }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex("^\\d{" + Numbers + "}$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
