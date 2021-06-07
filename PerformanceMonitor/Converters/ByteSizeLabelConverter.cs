using ByteSizeLib;
using System;
using System.Windows.Data;

namespace PerformanceMonitor.Converters
{
    public class ByteSizeLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is float size)
            {
                if (size > 128)
                {
                    return ByteSize.FromMegaBytes(size).ToString();
                }
                return ByteSize.FromGigaBytes(size).ToString();
            }

            return "n/a";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
