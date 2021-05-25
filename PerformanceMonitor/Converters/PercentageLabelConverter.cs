using System;
using System.Windows.Data;

namespace PerformanceMonitor.Converters
{
    public class PercentageLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is float percentage)
            {
                return $"{parameter}: {percentage:N2}%";
            }

            return $"{parameter}: n/a";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
