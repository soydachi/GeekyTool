// https://marcominerva.wordpress.com/2014/07/29/a-behavior-to-handle-visualstate-in-universal-apps-with-mvvm/

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace GeekyTool.UI.Behaviors
{
    public class EnumStateBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; set; }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(object), typeof(EnumStateBehavior), new PropertyMetadata(default(object), ValuePropertyChanged));

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as EnumStateBehavior;
            if (behavior?.AssociatedObject == null || e.NewValue == null) return;

            VisualStateManager.GoToState(behavior.AssociatedObject as Control, e.NewValue.ToString(), true);
        }

        public void Attach(DependencyObject associatedObject)
        {
            var control = associatedObject as Control;
            if (control == null)
                throw new ArgumentException("EnumStateBehavior can be attached only to control");

            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }
    }
}
