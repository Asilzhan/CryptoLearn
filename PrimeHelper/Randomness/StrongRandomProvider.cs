using System;
using System.Security.Cryptography;

namespace PrimeHelper.Randomness
{
	public class StrongRandomProvider : IRandomProvider
	{

		[ThreadStatic]
		private static RandomNumberGenerator _strongRandom;

		private static RandomNumberGenerator StrongRandom
			=> _strongRandom ??= RandomNumberGenerator.Create();

		[ThreadStatic] private static Random _fastRandom;

		private static Random FastRandom
			=> _fastRandom ??= new Random();

		public void NextBytes(byte[] buffer)
		{
			StrongRandom.GetBytes(buffer);
		}

		public int NextInt(int maxExclusive)
		{
			return FastRandom.Next(maxExclusive);
		}
	}
}