using System;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace GeekyTool.UI.Behaviors
{
    public class TrimTextBehavior : Behavior
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var textBox = AssociatedObject as TextBox;
            if (textBox == null)
                throw new InvalidOperationException("TrimTextBehavior can only be attached to TextBox controls");

            textBox.TextChanging += TextBoxOnTextChanging;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            var textBox = AssociatedObject as TextBox;
            if (textBox == null) return;

            textBox.TextChanging -= TextBoxOnTextChanging;
        }

        private void TextBoxOnTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            sender.Text = sender.Text.Trim();
        }
    }
}