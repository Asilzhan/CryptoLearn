using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace CryptoLearn.Converters
{
	// public class StringToBytesConverter : IValueConverter
	// {
	// 	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	// 	{
	// 		string str = value.ToString();
	// 		Encoding encoding = 
	// 		var t = Encoding.GetBytes(str);
	// 		int n = System.Convert.ToInt32(parameter.ToString());
	// 		var res = new byte[n];
	// 		for (int i = 0; i < n; i++)
	// 		{
	// 			res[i] = t[i % t.Length];
	// 		}
	//
	// 		return res;
	// 	}
	//
	// 	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	// 	{
	// 		throw new NotImplementedException();
	// 	}
	// }
}