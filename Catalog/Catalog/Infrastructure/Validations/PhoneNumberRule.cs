using System.Text.RegularExpressions;

namespace Catalog.Infrastructure.Validations
{
    public class PhoneNumberRule<T> : IValidationRule<T>
    {
        public string ValidationMessage => $"Поле {Name} должно иметь формат +375(17|25|29|33|44)0000000.";

        public string Name { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(@"^\+(375){1}(17|25|29|33|44){1}\d{7}$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
