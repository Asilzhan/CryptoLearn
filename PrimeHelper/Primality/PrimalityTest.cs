using System.Numerics;
using System.Threading.Tasks;
using PrimeHelper.Helpers;
using PrimeHelper.Randomness;
using PrimeHelper.Tools;

namespace PrimeHelper.Primality
{
	public abstract class PrimalityTest : IPrimalityTest
	{
		internal IRandomProvider RandomProviderInternal { private get; set; } = new StrongRandomProvider();

		protected IRandomProvider RandomProvider => this.RandomProviderInternal;

		protected Task<BigInteger> RandomIntegerBelowAsync(BigInteger max)
		{
			return RandomBigInteger.GenerateAsync(max, this.RandomProvider);
		}

		protected bool? CheckEdgeCases(BigInteger value)
		{
			return BigIntegerHelpers.TrivialCheck(value);
		}       

		public abstract Task<bool> TestAsync(BigInteger source);
	}
}