using System;
using System.Linq;
using System.Numerics;

namespace PrimeHelper.Helpers
{
	public class BigIntegerHelpers
	{
		public static readonly BigInteger Zero = BigInteger.Zero;
		public static readonly BigInteger One = BigInteger.One;
		public static readonly BigInteger Two = new BigInteger(2);

		public static bool? TrivialCheck(BigInteger source)
		{
			if (source.Sign < 0) throw new ArgumentOutOfRangeException(nameof(source), "Source number must be a positive.");

			if (source.Equals(One) || source.Equals(Zero))
				return false;
			if (source.Equals(Two)) return true;

			if (source.IsEven) return false;

			if (source < 1000)
			{
				return PrimeNumbers.KnownPrimes.Contains((int) source);
			}

			return PrimeNumbers.KnownPrimes.Any(p => BigInteger.Remainder(source, p) == 0)
				? false
				: (bool?)null;
		}
		public static BigInteger GcdEx(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y) {
			if (a == 0) {
				x = 0; y = 1;
				return b;
			}

			BigInteger x1 = 0, y1 = 0;
			BigInteger d = GcdEx(b%a, a, ref x1, ref y1);
			x = y1 - (b / a) * x1;
			y = x1;
			return d;
		}
	}
	
}