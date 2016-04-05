using System;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
                return !(bool) value;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
                return !(bool) value;
            return true;
        }
    }
}