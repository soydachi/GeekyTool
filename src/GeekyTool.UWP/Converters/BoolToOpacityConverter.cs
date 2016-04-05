using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = (bool?) value;
            double parameterValue;

            parameterValue = double.Parse((string) parameter, CultureInfo.InvariantCulture);

            if (boolValue.HasValue)
            {
                if (boolValue.Value)
                    return parameterValue;
                return 1;
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}