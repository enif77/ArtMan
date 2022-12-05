/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;


    public sealed class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? "ano" : "ne";
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
