using System;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //No value provided
            if (value == null)
            {
                return null;
            }
            // No format provided.
            if (parameter == null)
            {
                return value;
            }

            return string.Format((string) parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}