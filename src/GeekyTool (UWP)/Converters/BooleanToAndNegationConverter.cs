using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GeekyTool.Converters
{
    public class BooleanToAndNegationConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty BindableBooleanProperty = DependencyProperty.Register(
            "BindableBoolean", typeof(bool), typeof(BooleanToAndNegationConverter), new PropertyMetadata(default(bool)));

        public bool BindableBoolean
        {
            get { return (bool) GetValue(BindableBooleanProperty); }
            set { SetValue(BindableBooleanProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = (bool)value;

            if (!boolValue & !BindableBoolean)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var boolValue = (bool)value;

            if (!boolValue & !BindableBoolean)
            {
                return true;
            }
            return false;
        }
    }
}
