using System.Collections.Generic;

namespace Catalog.Infrastructure.Validations
{
    public class NumberRangeRule<T> : IValidationRule<T> 
    {
        public string ValidationMessage => $"The {Name} must be from {Min} to {Max}.";

        public string Name { get; set; }

        public T Min { get; set; }

        public T Max { get; set; }

        public NumberRangeRule(T min, T max) 
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
