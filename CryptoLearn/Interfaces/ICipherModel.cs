using System;
using System.Windows.Documents;

namespace CryptoLearn.Interfaces
{
	public interface ICipherModel
	{
		public byte[] Key { get; set; }
		public Span<byte> Encrypt(Span<byte> data);
		public Span<byte> Decrypt(Span<byte> data);

		public string BytesToString(Span<byte> data);
		public Span<byte> StringToBytes(string str);
	}
}