using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace G_RPC
{
    public class DarkenColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                Color originalColor = brush.Color;
                Color darkenedColor = Color.FromArgb(originalColor.A, (byte)(originalColor.R * 0.8), (byte)(originalColor.G * 0.8), (byte)(originalColor.B * 0.8));
                return new SolidColorBrush(darkenedColor);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
