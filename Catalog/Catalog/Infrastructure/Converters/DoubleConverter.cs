using System;
using System.Globalization;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Converters
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double ? value.ToString() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return double.TryParse(value as string, NumberStyles.Any, culture, out var resultValue) ? resultValue : value;
        }
    }
}
