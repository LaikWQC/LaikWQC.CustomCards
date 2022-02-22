using System;
using System.Globalization;
using System.Windows.Data;

namespace LaikWQC.CustomCards.Wpf
{
    public class NullableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && string.IsNullOrEmpty(str)) return null;
            return value;
        }
    }
}
