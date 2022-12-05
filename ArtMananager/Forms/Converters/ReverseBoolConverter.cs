/* (C) 2016 Přemysl Fára */

namespace ArtMananager.Forms.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;


    public sealed class ReverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !System.Convert.ToBoolean(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !System.Convert.ToBoolean(value);
        }
    }
}
