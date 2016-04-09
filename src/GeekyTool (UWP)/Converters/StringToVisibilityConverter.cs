using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class StringToVisibilityConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty SetToCollapsedProperty = DependencyProperty.Register(
            "SetToCollapsed", typeof (bool), typeof (StringToVisibilityConverter), new PropertyMetadata(default(bool)));

        public bool SetToCollapsed
        {
            get { return (bool) GetValue(SetToCollapsedProperty); }
            set { SetValue(SetToCollapsedProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;

            // SetToCollapsed = true --> Collapsed
            // SetToCollapsed = false --> Visible
            if (SetToCollapsed)
                return Visibility.Collapsed;

            if (value is string)
            {
                var result = (string) value;
                return string.IsNullOrEmpty(result) ? Visibility.Collapsed : Visibility.Visible;
            }
            else if (value is string[])
            {
                var result = (string[]) value;
                
                return result.All(string.IsNullOrEmpty) ? Visibility.Collapsed : Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
