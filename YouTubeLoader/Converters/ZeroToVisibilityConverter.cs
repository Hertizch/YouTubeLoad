using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YouTubeLoader.Converters
{
    /// <summary>
    /// Converts Zero (0) Values to Control.Visibility values
    /// </summary>
    public class ZeroToVisibilityConverter : IValueConverter
    {
        //Set to true if you want to show control when boolean value is true
        //Set to false if you want to hide/collapse control when value is true
        public bool TriggerValue { get; set; }

        //Set to true if you just want to hide the control
        //else set to false if you want to collapse the control
        public bool IsHidden { get; set; }

        private object GetVisibility(object value)
        {
            if (!(value is int))
                return DependencyProperty.UnsetValue;

            var objValue = (int)value;

            if ((objValue <= 0 && TriggerValue && IsHidden) || (objValue > 0 && !TriggerValue && IsHidden))
            {
                return Visibility.Hidden;
            }

            if ((objValue <= 0 && TriggerValue && !IsHidden) || (objValue > 0 && !TriggerValue && !IsHidden))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
