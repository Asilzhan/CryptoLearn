using System;
using System.Numerics;
using System.Text;

namespace CryptoLearn.Helper
{
	public static class CharExtensions
	{
		public static char Capitalize(this char from, char to)
		{
			if (char.IsUpper(to)) return char.ToUpper(from);
			return from;
		}
	}
	public static class ArrayExtensions
	{

	}
}