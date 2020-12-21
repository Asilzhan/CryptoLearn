using System;
using System.Numerics;
using System.Text;

namespace CryptoLearn.Interfaces
{
	public interface IRsaModel
	{
		public ulong P { get; set; }
		public ulong Q { get; set; }
		public ulong E { get; set; }
		public ulong D { get; set; }
		public ulong Totient { get; }
		public ulong N { get; }
		public ulong[] Encrypt(ulong[] arr);
		public ulong[] Decrypt(ulong[] arr);
		public ulong[] StringToArray(string s, Encoding encoding);
		public string ArrayToString(ulong[] b, Encoding encoding);
		public void GeneratePrimes();
		public void CalculateDAndE();
	}
}