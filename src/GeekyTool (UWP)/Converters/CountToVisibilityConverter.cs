using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int result;
            int.TryParse(value.ToString(), out result);

            if (result == 0)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}