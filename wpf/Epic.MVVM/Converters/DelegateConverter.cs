using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Epic.MVVM.Converters
{
    public class DelegateConverter<T> : IValueConverter
    {

        public DelegateConverter(Func<object, T> converter, Func<T, object> converterBack)
        {
            if (converter != null)
                this.Converter = (value, target, parameter, culture) => converter(value);

            if (converterBack != null)
                this.ConverterBack = (value, target, parameter, culture) => converterBack((T)value);
        }

        Func<object, Type, object, CultureInfo, object> Converter;
        Func<object, Type, object, CultureInfo, object> ConverterBack;
    

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Converter == null) return default(T);
            return this.Converter(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.ConverterBack == null) return null;
            return this.ConverterBack(value, targetType, parameter, culture);
        }

    }
}
