using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var nullable = value as bool?;
            if (nullable.HasValue && nullable.Value)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}