using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Stanley_Utility
{
    public enum MaskType
    {
        Any,
        Integer,
        Decimal
    }

    public class TextBoxMaskBehavior
    {
        public static double GetMinimumValue(DependencyObject obj)
        {
            return (double)obj.GetValue(TextBoxMaskBehavior.MinimumValueProperty);
        }

        public static void SetMinimumValue(DependencyObject obj, double value)
        {
            obj.SetValue(TextBoxMaskBehavior.MinimumValueProperty, value);
        }

        private static void MinimumValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox @this = d as TextBox;
            TextBoxMaskBehavior.ValidateTextBox(@this);
        }

        public static double GetMaximumValue(DependencyObject obj)
        {
            return (double)obj.GetValue(TextBoxMaskBehavior.MaximumValueProperty);
        }

        public static void SetMaximumValue(DependencyObject obj, double value)
        {
            obj.SetValue(TextBoxMaskBehavior.MaximumValueProperty, value);
        }

        private static void MaximumValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox @this = d as TextBox;
            TextBoxMaskBehavior.ValidateTextBox(@this);
        }

        public static MaskType GetMask(DependencyObject obj)
        {
            return (MaskType)obj.GetValue(TextBoxMaskBehavior.MaskProperty);
        }

        public static void SetMask(DependencyObject obj, MaskType value)
        {
            obj.SetValue(TextBoxMaskBehavior.MaskProperty, value);
        }

        private static void MaskChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is TextBox)
            {
                (e.OldValue as TextBox).PreviewTextInput -= TextBoxMaskBehavior.TextBox_PreviewTextInput;
                (e.OldValue as TextBox).LostFocus -= TextBoxMaskBehavior.TextBox_LostFocus;
                DataObject.RemovePastingHandler(e.OldValue as TextBox, new DataObjectPastingEventHandler(TextBoxMaskBehavior.TextBoxPastingEventHandler));
            }
            TextBox textBox = d as TextBox;
            if (textBox != null)
            {
                if ((MaskType)e.NewValue != MaskType.Any)
                {
                    textBox.PreviewTextInput += TextBoxMaskBehavior.TextBox_PreviewTextInput;
                    textBox.LostFocus += TextBoxMaskBehavior.TextBox_LostFocus;
                    textBox.KeyDown += TextBoxMaskBehavior.TextBox_KeyDown;
                    DataObject.AddPastingHandler(textBox, new DataObjectPastingEventHandler(TextBoxMaskBehavior.TextBoxPastingEventHandler));
                }
                TextBoxMaskBehavior.ValidateTextBox(textBox);
            }
        }

        private static void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                TextBoxMaskBehavior.TextBox_LostFocus(sender, null);
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text;
            bool flag = TextBoxMaskBehavior.IsSymbolValid(TextBoxMaskBehavior.GetMask(textBox), text);
            switch (TextBoxMaskBehavior.GetMask(textBox))
            {
                case MaskType.Integer:
                    {
                        int num = 0;
                        int num2 = (int)TextBoxMaskBehavior.GetMinimumValue(textBox);
                        int num3 = (int)TextBoxMaskBehavior.GetMaximumValue(textBox);
                        if (!int.TryParse(text, out num))
                        {
                            num = num2;
                        }
                        if (num < num2)
                        {
                            num = num2;
                        }
                        else if (num > num3)
                        {
                            num = num3;
                        }
                        text = num.ToString();
                        break;
                    }
                case MaskType.Decimal:
                    {
                        double num4 = 0.0;
                        double minimumValue = TextBoxMaskBehavior.GetMinimumValue(textBox);
                        double maximumValue = TextBoxMaskBehavior.GetMaximumValue(textBox);
                        if (!double.TryParse(text, out num4))
                        {
                            num4 = minimumValue;
                        }
                        if (num4 < minimumValue)
                        {
                            num4 = minimumValue;
                        }
                        else if (num4 > maximumValue)
                        {
                            num4 = maximumValue;
                        }
                        if (num4 == double.NaN)
                        {
                            num4 = 0.0;
                        }
                        text = num4.ToString();
                        break;
                    }
            }
            textBox.Text = text;
        }

        private static void ValidateTextBox(TextBox _this)
        {
            if (TextBoxMaskBehavior.GetMask(_this) != MaskType.Any)
            {
                _this.Text = TextBoxMaskBehavior.ValidateValue(TextBoxMaskBehavior.GetMask(_this), _this.Text, TextBoxMaskBehavior.GetMinimumValue(_this), TextBoxMaskBehavior.GetMaximumValue(_this));
            }
        }

        private static void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = e.DataObject.GetData(typeof(string)) as string;
            text = TextBoxMaskBehavior.ValidateValue(TextBoxMaskBehavior.GetMask(textBox), text, TextBoxMaskBehavior.GetMinimumValue(textBox), TextBoxMaskBehavior.GetMaximumValue(textBox));
            if (!string.IsNullOrEmpty(text))
            {
                textBox.Text = text;
            }
            e.CancelCommand();
            e.Handled = true;
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox obj = sender as TextBox;
            bool flag = TextBoxMaskBehavior.IsSymbolValid(TextBoxMaskBehavior.GetMask(obj), e.Text);
            e.Handled = !flag;
            if (flag)
            {
            }
        }

        private static string ValidateValue(MaskType mask, string value, double min, double max)
        {
            string result;
            if (string.IsNullOrEmpty(value))
            {
                result = string.Empty;
            }
            else
            {
                value = value.Trim();
                switch (mask)
                {
                    case MaskType.Integer:
                        try
                        {
                            Convert.ToInt64(value);
                            result = value;
                            break;
                        }
                        catch
                        {
                        }
                        result = string.Empty;
                        break;
                    case MaskType.Decimal:
                        try
                        {
                            Convert.ToDouble(value);
                            result = value;
                            break;
                        }
                        catch
                        {
                        }
                        result = string.Empty;
                        break;
                    default:
                        result = value;
                        break;
                }
            }
            return result;
        }

        private static double ValidateLimits(double min, double max, double value)
        {
            if (!min.Equals(double.NaN))
            {
                if (value < min)
                {
                    return min;
                }
            }
            if (!max.Equals(double.NaN))
            {
                if (value > max)
                {
                    return max;
                }
            }
            return value;
        }

        private static bool IsSymbolValid(MaskType mask, string str)
        {
            switch (mask)
            {
                case MaskType.Any:
                    return true;
                case MaskType.Integer:
                    if (str == NumberFormatInfo.CurrentInfo.NegativeSign)
                    {
                        return true;
                    }
                    break;
                case MaskType.Decimal:
                    if (str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator || str == NumberFormatInfo.CurrentInfo.NegativeSign)
                    {
                        return true;
                    }
                    break;
            }
            bool result;
            if (mask.Equals(MaskType.Integer) || mask.Equals(MaskType.Decimal))
            {
                foreach (char c in str)
                {
                    if (!char.IsDigit(c))
                    {
                        return false;
                    }
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static readonly DependencyProperty MinimumValueProperty = DependencyProperty.RegisterAttached("MinimumValue", typeof(double), typeof(TextBoxMaskBehavior), new FrameworkPropertyMetadata(double.NaN, new PropertyChangedCallback(TextBoxMaskBehavior.MinimumValueChangedCallback)));

        public static readonly DependencyProperty MaximumValueProperty = DependencyProperty.RegisterAttached("MaximumValue", typeof(double), typeof(TextBoxMaskBehavior), new FrameworkPropertyMetadata(double.NaN, new PropertyChangedCallback(TextBoxMaskBehavior.MaximumValueChangedCallback)));

        public static readonly DependencyProperty MaskProperty = DependencyProperty.RegisterAttached("Mask", typeof(MaskType), typeof(TextBoxMaskBehavior), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextBoxMaskBehavior.MaskChangedCallback)));
    }
}
