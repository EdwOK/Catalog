using System;
using System.Globalization;
using Xamarin.Forms;

namespace Catalog.Infrastructure.Converters
{
    public class SelectedItemChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as SelectedItemChangedEventArgs;

            if (eventArgs == null)
            {
                throw new ArgumentException("Expected SelectedEventArgs as value", "value");
            }

            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
