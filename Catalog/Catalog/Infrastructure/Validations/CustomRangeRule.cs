using System.Collections.Generic;

namespace Catalog.Infrastructure.Validations
{
    public class CustomRangeRule<T> : IValidationRule<T> 
    {
        public string ValidationMessage => $"Поле {Name} должно иметь значение в пределах от {Min} до {Max}.";

        public string Name { get; set; }

        public T Min { get; set; }

        public T Max { get; set; }

        public CustomRangeRule(T min, T max) 
        {
            Min = min;
            Max = max;
        }

        public bool Check(T value)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            bool moreMin = comparer.Compare(value, Min) >= 0;
            bool lessMax = comparer.Compare(value, Max) <= 0;

            return moreMin && lessMax;
        }
    }
}
