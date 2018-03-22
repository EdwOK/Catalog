using System.Text.RegularExpressions;

namespace Catalog.Infrastructure.Validations
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage => $"The {Name} has an invalid format.";

        public string Name { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}
