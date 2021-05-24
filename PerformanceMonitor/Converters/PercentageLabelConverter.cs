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
                var calcPerc = percentage;
                if (percentage > 1)
                {
                    calcPerc = percentage / 100;
                }
                return $"Used: {calcPerc:P2}";
            }

            return "Used: n/a";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
