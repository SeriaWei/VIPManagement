using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Easy.WPF.ValueConverter
{
    public class DictionaryValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dict = parameter as Dictionary<string, string>;
            if (value != null && dict != null && dict.ContainsKey(value.ToString()))
            {
                return dict[value.ToString()];
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is KeyValuePair<string, string>)
            {
                return ((KeyValuePair<string, string>)value).Key;
            }
            return value;
        }
    }
}
