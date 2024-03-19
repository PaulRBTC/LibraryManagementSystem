using System;
using System.Globalization;
using System.Windows.Data;

namespace LibraryManagementSystem.Converters
{
    public class BoolToCheckmarkConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isTrue)
            {
                return isTrue ? "✔" : "❌";
            }
            else
            {
                return "❌";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value}" != "❌";
        }

    }
}
