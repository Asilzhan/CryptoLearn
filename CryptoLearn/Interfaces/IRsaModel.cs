using System;
using System.Numerics;
using System.Text;

namespace CryptoLearn.Interfaces
{
	public interface IRsaModel
	{
		public BigInteger P { get; set; }
		public BigInteger Q { get; set; }
		public BigInteger E { get; set; }
		public BigInteger D { get; set; }
		public BigInteger Totient { get; }
		public BigInteger N { get; }
		public string Alphabet { get; }
		public ulong[] Encrypt(ulong[] arr);
		public ulong[] Decrypt(ulong[] arr);
		public ulong[] StringToULongArray(string s, Encoding encoding);
		public string ULongArrayToString(ulong[] b, Encoding encoding);
		public void GeneratePrimes();
		public void CalculateDAndE();
	}
}