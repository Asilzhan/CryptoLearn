using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoLearn.Converters
{
    public class SpacedCapitalConverter : IValueConverter    
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value as string;
            string res = "";
            for (int i = 0; i < text.Length; i++)
            {
                res += char.ToUpper(text[i]);
                res += " ";
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}