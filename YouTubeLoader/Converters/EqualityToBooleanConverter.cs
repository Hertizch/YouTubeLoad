using System;
using System.Globalization;
using System.Windows.Data;

namespace YouTubeLoader.Converters
{
    public class EqualityToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                throw new ArgumentException(@"EqualityToBooleanConverter expects a input value", nameof(values));

            return values[0].Equals(values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
