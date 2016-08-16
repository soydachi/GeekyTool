using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GeekyTool.Extensions;
using Microsoft.Xaml.Interactivity;

namespace GeekyTool.UI.Behaviors
{
    public class ClearUglyTextBehavior : Behavior
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var associatedTextBox = AssociatedObject as TextBox;
            if (associatedTextBox == null)
                throw new InvalidOperationException("ClearUglyTextBehavior can only be attached to TextBox controls");

            associatedTextBox.LostFocus += TextBoxOnLostFocus;
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching();

            var associatedTextBox = AssociatedObject as TextBox;
            if (associatedTextBox == null) return;

            associatedTextBox.LostFocus -= TextBoxOnLostFocus;
        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs e)
        {
            var associatedTextBox = AssociatedObject as TextBox;
            associatedTextBox.Text = associatedTextBox.Text.Clean();
        }
    }
}