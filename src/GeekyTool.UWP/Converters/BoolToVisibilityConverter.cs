using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var nullable = value as bool?;
            if (nullable.HasValue && nullable.Value)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}