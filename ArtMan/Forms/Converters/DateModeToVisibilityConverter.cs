/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using ArtMan.Forms.Controls;


    public sealed class DateModeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (EzpDatePickerMode)value;
            return (data == EzpDatePickerMode.Mandatory /*|| data == OptDatePickerMode.AllowEmpty*/) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
