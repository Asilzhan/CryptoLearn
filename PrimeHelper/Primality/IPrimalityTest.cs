using System.Numerics;
using System.Threading.Tasks;

namespace PrimeHelper.Primality
{
	public interface IPrimalityTest
	{
		Task<bool> TestAsync(BigInteger source);
	}
}