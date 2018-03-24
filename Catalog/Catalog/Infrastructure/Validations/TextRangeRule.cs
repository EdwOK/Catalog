namespace Catalog.Infrastructure.Validations
{
    public class TextRangeRule<T> : IValidationRule<T>
    {
        public string ValidationMessage => $"Поле {Name} должно быть в пределах от {Min} до {Max} символов.";

        public int Min { get; set; }

        public int Max { get; set; }

        public string Name { get; set; }

        public TextRangeRule(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            var length = str?.Length;
            if (length < Min || length > Max)
            {
                return false;
            }

            return true;
        }
    }
}
