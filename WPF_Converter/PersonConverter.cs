using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_Converter
{
    [ValueConversion(typeof(String), typeof(String))]
    class PersonConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)values[0] + ", " + (string)values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
