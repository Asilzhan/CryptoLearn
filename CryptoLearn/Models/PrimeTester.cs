using System;
using System.Numerics;

namespace CryptoLearn.Models
{
	public static class PrimeTester
	{
		private const int Trivial_limit = 50;

		private static BigInteger[] Primes = new BigInteger[1000];

		// public static bool IsPrime(BigInteger n)
		// {
		// 	if (n < (long) 1e10)
		// 	{
		// 		for (int i = 0; i * i < n; i++)
		// 		{
		// 			if (n % i == 0)
		// 				return true;
		// 		}
		//
		// 		return false;
		// 	}
		//
		// }
		static PrimeTester()
		{
			for (int i = 2, j = 0; j < Trivial_limit; ++i)
			{
				bool pr = true;
				for (int k = 2; k * k <= i; ++k)
					if (i % k == 0)
						pr = false;
				if (pr)
					Primes[j++] = i;
			}
		}

		public static bool IsPrime(BigInteger n)
		{
			for (int i = 0; i < Trivial_limit && Primes[i] < n; ++i)
				if (n % Primes[i] == 0)
					return false;
			if (Primes[Trivial_limit - 1] * Primes[Trivial_limit - 1] >= n)
				return true;
			if (!MillerRabinTest(n))
				return false;
			return Bpsw(n);
		}

		public static bool MillerRabinTest(BigInteger n)
		{
			BigInteger b = 2;
			for (BigInteger g; (g = BigInteger.GreatestCommonDivisor(n, b)) != 1; ++b)
				if (n > g)
					return false;
			BigInteger p = 0, q = n - 1;
			while ((q & 1) == 0)
			{
				p++;
				q >>= 1;
			}

			BigInteger rem = BigInteger.ModPow(b, q, n);
			if (rem == 1 || rem == n - 1)
				return true;
			for (BigInteger i = 1; i < p; ++i)
			{
				rem = (rem * rem) % n;
				if (rem == n - 1)
					return true;
			}

			return false;
		}

		public static int Jacobi(BigInteger a, BigInteger b)
		{
			if (a == 0) return 0;
			if (a == 1) return 1;
			if (a < 0)
				if ((b & 2) == 0)
					return Jacobi(-a, b);
				else
					return -Jacobi(-a, b);
			BigInteger a1 = a, e = 0;
			while ((a1 & 1) == 0)
			{
				a1 >>= 1;
				++e;
			}

			int s;
			if ((e & 1) == 0 || (b & 7) == 1 || (b & 7) == 7)
				s = 1;
			else
				s = -1;
			if ((b & 3) == 3 && (a1 & 3) == 3)
				s = -s;
			if (a1 == 1)
				return s;
			return s * Jacobi(b % a1, a1);
		}

		public static bool Bpsw(BigInteger n)
		{
			if (n.Sqrt() * n.Sqrt() == n) return false;
			BigInteger dd = 5;
			for (;;)
			{
				BigInteger g = BigInteger.GreatestCommonDivisor(n, BigInteger.Abs(dd));
				if (1 < g && g < n) return false;
				if (Jacobi(dd, n) == -1) break;
				dd = dd < 0 ? -dd + 2 : -dd - 2;
			}

			BigInteger p = 1, q = (p * p - dd) / 4;
			BigInteger d = n + 1, s = 0;
			while ((d & 1) == 0)
			{
				s++;
				d >>= 1;
			}

			BigInteger u = 1, v = p, u2m = 1, v2m = p, qm = q, qm2 = q * 2, qkd = q;
			for (int mask = 2; mask <= d; mask <<= 1)
			{
				u2m = (u2m * v2m) % n;
				v2m = (v2m * v2m) % n;
				while (v2m < qm2) v2m += n;
				v2m -= qm2;
				qm = (qm * qm) % n;
				qm2 = qm * 2;
				if ((d & mask) != 0)
				{
					BigInteger t1 = (u2m * v) % n,
						t2 = (v2m * u) % n,
						t3 = (v2m * v) % n,
						t4 = (((u2m * u) % n) * dd) % n;
					u = t1 + t2;
					if ((u & 1) != 0) u += n;
					u = (u >> 1) % n;
					v = t3 + t4;
					if ((v & 1) != 0) v += n;
					v = (v >> 1) % n;
					qkd = (qkd * qm) % n;
				}
			}

			if (u == 0 || v == 0) return true;
			BigInteger qkd2 = qkd * 2;
			for (int r = 1; r < s; ++r)
			{
				v = (v * v) % n - qkd2;
				if (v < 0) v += n;
				if (v < 0) v += n;
				if (v >= n) v -= n;
				if (v >= n) v -= n;
				if (v == 0) return true;
				if (r < s - 1)
				{
					qkd = (qkd * qkd) % n;
					qkd2 = qkd * 2;
				}
			}

			return false;
		}

		public static BigInteger Sqrt(this BigInteger n)
		{
			if (n == 0) return 0;
			if (n > 0)
			{
				int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
				BigInteger root = BigInteger.One << (bitLength / 2);

				while (!IsSqrt(n, root))
				{
					root += n / root;
					root /= 2;
				}

				return root;
			}

			throw new ArithmeticException("NaN");
		}

		private static Boolean IsSqrt(BigInteger n, BigInteger root)
		{
			BigInteger lowerBound = root * root;
			BigInteger upperBound = (root + 1) * (root + 1);

			return (n >= lowerBound && n < upperBound);
		}
	}
}