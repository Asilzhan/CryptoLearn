using System;
using System.Numerics;
using System.Threading.Tasks;
using PrimeHelper.Helpers;

namespace PrimeHelper.Primality.Heuristic
{
	public class FermatTest : PrimalityTest
	{
		private readonly uint _complexity;

		public FermatTest(uint complexity)
		{
			if (complexity == 0)
				throw new ArgumentOutOfRangeException(nameof(complexity), "Complexity must be above 0.");

			_complexity = complexity;
		}

		public override async Task<bool> TestAsync(BigInteger source)
		{
			var trivialCheck = this.CheckEdgeCases(source);
			if (trivialCheck.HasValue) return trivialCheck.Value;

			for (var i = 0; i < _complexity; i++)
			{
				var randomNumber = await this.RandomIntegerBelowAsync(source);
				randomNumber = BigInteger.ModPow(randomNumber, source - 1, source);

				if (!randomNumber.Equals(BigIntegerHelpers.One)) return false;
			}

			return true;
		}
	}
}