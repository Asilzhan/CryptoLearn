using System;
using System.Globalization;
using System.Windows.Data;

namespace CryptoLearn.Converters
{
    public class SpacedCapitalConverter : IValueConverter    
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {            
            string res = "";
            if (value is string)
            {
                string text = value as string;
                for (int i = 0; i < text.Length; i++)
                {
                    res += char.ToUpper(text[i]);
                    res += " ";
                }
            }

            if (value is ulong[])
            {
                ulong[] ulongs = value as ulong[];
                foreach (var t in ulongs)
                {
                    res += t;
                    res += "#";
                }
            }
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}