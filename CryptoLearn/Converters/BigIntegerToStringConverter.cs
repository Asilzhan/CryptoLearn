using System;
using System.Globalization;
using System.Numerics;
using System.Windows.Data;

namespace CryptoLearn.Converters
{
	public class BigIntegerToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value?.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return BigInteger.Parse(value?.ToString()??"0");
		}
	}
}