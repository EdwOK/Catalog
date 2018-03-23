using System;
using System.Globalization;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Converters
{
    public class InverseCountToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
            {
                return value;
            }

            int count = System.Convert.ToInt32(value);

            return count == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
