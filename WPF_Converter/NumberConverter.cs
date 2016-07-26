using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_Converter
{
    [ValueConversion(typeof(Double), typeof(String))]
    public class NumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = parameter as string;
            return ((double)value).ToString(param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string amount = value as string;
            return System.Convert.ToDouble(amount);
        }
    }
}
