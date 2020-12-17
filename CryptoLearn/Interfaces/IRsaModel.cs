using System.Numerics;

namespace CryptoLearn.Interfaces
{
	public interface IRsaModel
	{
		public BigInteger P { get; set; }
		public BigInteger Q { get; set; }
		public BigInteger E { get; set; }
		public BigInteger D { get; set; }
		public BigInteger Totient { get; set; }
		public BigInteger N { get; set; }
		public string Alphabet { get; set; }
		public string PlainText { get; set; }
		public string CipherText { get; set; }
		public void Encrypt();
		public void Decrypt();
		public void GeneratePrimes();
		public void CalculateDAndE();
	}
}