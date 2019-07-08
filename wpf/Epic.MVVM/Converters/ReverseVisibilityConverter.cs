using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Epic.MVVM.Converters
{
    public sealed class ReverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility b)
                return b == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility b)
                return b == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            return Visibility.Collapsed;
        }
    }
}
