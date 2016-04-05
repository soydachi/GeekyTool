using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace GeekyTool.Converters
{
    public class StringToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var retVal = string.Empty;
            if (value is string)
            {
                retVal = (string) value;
                return new SolidColorBrush(
                    Color.FromArgb(
                        255,
                        System.Convert.ToByte(retVal.Substring(1, 2), 16),
                        System.Convert.ToByte(retVal.Substring(3, 2), 16),
                        System.Convert.ToByte(retVal.Substring(5, 2), 16)
                        )
                    );
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
