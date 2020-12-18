using System.Numerics;

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
		public string Alphabet { get; set; }
		public string PlainText { get; set; }
		public string CipherText { get; set; }
		public byte[] Encrypt(byte[] arr);
		public byte[] Decrypt(byte[] arr);
		public byte[] StringToByteArray(string s);
		public string ByteArrayToString(byte[] b);
		public void GeneratePrimes();
		public void CalculateDAndE();
	}
}